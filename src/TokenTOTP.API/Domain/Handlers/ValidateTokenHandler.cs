﻿using MediatR;
using Responses;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.API.Domain.Model.View;
using TokenTOTP.API.Infra.Data.Repositories;

namespace TokenTOTP.API.Domain.Handlers
{
    public class ValidateTokenHandler : IRequestHandler<ValidateTokenRequest, Result>
    {
        private readonly ITokenTotpRepository _repository;

        public ValidateTokenHandler(ITokenTotpRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ValidateTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetTokenAsync(request.Token, request.Hash, cancellationToken: cancellationToken);

            if (result == null)
                return Result.Fail(ErrorMessages.TokenNotFound.code, ErrorMessages.TokenNotFound.message);
            else
            {
                if (result.IsExpired())
                    return Result.Fail(ErrorMessages.ExpiredToken.code, ErrorMessages.ExpiredToken.message);
                return Result.Ok();
            }
        }
    }
}