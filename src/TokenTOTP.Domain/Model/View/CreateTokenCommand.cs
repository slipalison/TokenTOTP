using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using Responses;
using TokenTOTP.Domain.Validations;
using TokenTOTP.Shared.ViewModel;

namespace TokenTOTP.Domain.Model.View
{
    public class CreateTokenCommand : Shared.ViewModel.CreateTokenCommand, IRequest<Result<TokenResponse>>, ITokenCommand
    {
        public CreateTokenCommand(string tokenType, int? timeToLive, string seed) : base(tokenType, timeToLive, seed)
        {
            TokenType = tokenType;
            TimeToLive = timeToLive;
            Seed = seed;
        }

        public bool IsValid() => new CreateTokenCommandValidation().Validate(this).IsValid;
        public ValidationResult ValidationResult() => new CreateTokenCommandValidation().Validate(this);
    }
}