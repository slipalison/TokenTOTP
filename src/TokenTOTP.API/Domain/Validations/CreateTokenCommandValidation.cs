using TokenTOTP.API.Domain.Model.View;

namespace TokenTOTP.API.Domain.Validations
{
    public class CreateTokenCommandValidation : TokenValidation<CreateTokenCommand>
    {
        public CreateTokenCommandValidation()
        {
            ValidateTokenType();
        }
    }
}