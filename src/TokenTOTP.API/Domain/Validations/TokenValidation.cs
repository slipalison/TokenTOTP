using FluentValidation;

using TokenTOTP.Shared.ViewModel;

namespace TokenTOTP.API.Domain.Validations
{
    public abstract class TokenValidation<T> : AbstractValidator<T> where T : TokenCommand
    {
        protected void ValidateTokenType()
        {
            RuleFor(c => c.TokenType)
                .NotEmpty().WithMessage("Please ensure you have entered the tokenType");
        }
    }
}