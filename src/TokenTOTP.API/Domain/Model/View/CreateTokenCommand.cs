using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using Responses;
using TokenTOTP.API.Domain.Validations;

namespace TokenTOTP.API.Domain.Model.View
{
    public class CreateTokenCommand : Shared.ViewModel.CreateTokenCommand, IRequest<Result<TokenResponse>>
    {
        public CreateTokenCommand(string tokenType, int? timeToLive, string seed) : base(tokenType, timeToLive, seed)
        {
            TokenType = tokenType;
            TimeToLive = timeToLive;
            Seed = seed;
        }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        public bool IsValid()
        {
            ValidationResult = new CreateTokenCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}