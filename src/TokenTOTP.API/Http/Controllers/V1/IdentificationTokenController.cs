using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Responses;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Domain.Model.View;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace TokenTOTP.API.Http.Controllers.V1
{
    [ApiController]
    [Route("api/V{version:apiVersion}/[controller]")]
    public class IdentificationTokenController : Controller
    {
        private readonly IMediator _mediator;

        public IdentificationTokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a TOTP token and save your payload for a future validation
        /// </summary>
        /// <param name="command">Payload data for token creation</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Created token and your TTL</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(TokenResponse), Status200OK)]
        [ProducesResponseType(typeof(AggregatedError), Status400BadRequest)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ActionResult<TokenResponse>> CreateToken([FromBody]CreateTokenCommand command, [FromHeader(Name = "X-Correlation-ID")]string correlationId, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                var errors = command.ValidationResult().Errors.Select(err => new
                {
                    err.PropertyName,
                    err.ErrorMessage
                }).ToDictionary(d => d.PropertyName, d => d.ErrorMessage).ToList();

                var error = new AggregatedError
                {
                    Code = ErrorMessages.InputInvalid.code,
                    Message = ErrorMessages.InputInvalid.message,
                    Errors = errors
                };

                return BadRequest(error);
            }

            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Validate token
        /// </summary>
        /// /// <param name="request">Token validation data</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Payload found</returns>
        [HttpPost("Validate")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(typeof(Error), Status400BadRequest)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ActionResult<string>> ValidateToken([FromBody]ValidateTokenRequest request, [FromHeader(Name = "X-Correlation-ID")]string correlationId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.IsSuccess)
            {
                if (result.Error.Code == ErrorMessages.TokenNotFound.code)
                    return NotFound(result.Error);

                return BadRequest(result.Error);
            }

            return Ok();
        }

        protected string GetCorrelationId()
        {
            StringValues correlationId = string.Empty;
            if (!HttpContext.Request.Headers.TryGetValue("X-Correlation-ID", out correlationId))
                return Guid.NewGuid().ToString();

            return correlationId.ToString();
        }
    }
}