using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using OtpNet;
using Responses;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Domain.Model.View;
using TokenTOTP.Infra.Configurations;
using TokenTOTP.Domain.Repositories;
using TokenTOTP.Domain.Services;

namespace TokenTOTP.Domain.Handlers
{
    public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, Result<TokenResponse>>
    {
        private readonly DefaultTimeToptOptions _defaultTimeTopt;

        private readonly OtpNetService _service;
        private readonly ITokenTotpRepository _repository;
        private readonly IMapper _mapper;

        public CreateTokenHandler(OtpNetService service, ITokenTotpRepository repository, IMapper mapper, IOptions<DefaultTimeToptOptions> toptOtions)
        {
            _defaultTimeTopt = toptOtions.Value;
            _service = service;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var seedBytes = _service.CreateSeed().seedBytes;

            if (!string.IsNullOrEmpty(request.Seed))
                seedBytes = Encoding.UTF8.GetBytes(request.Seed + request.TokenType);

            var token = _service.GenerateToken(seedBytes, _defaultTimeTopt.Range, _defaultTimeTopt.Size);

            var ttl = request.TimeToLive ?? token.timeToLive;
            var result = new TokenResponse
            {
                Hash = Base32Encoding.ToString(seedBytes),
                TimeToLive = ttl,
                Token = token.token
            };
            await _repository.InsertTokenAsync(result.Hash, result.Token, request.TokenType, result.TimeToLive, cancellationToken);

            return Result.Ok(result);
        }
    }
}