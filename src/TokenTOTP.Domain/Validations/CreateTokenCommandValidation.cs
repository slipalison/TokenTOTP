using TokenTOTP.Domain.Model.View;

namespace TokenTOTP.Domain.Validations
{
    public class CreateTokenCommandValidation : TokenValidation<CreateTokenCommand>
    {
        public CreateTokenCommandValidation()
        {
            ValidateTokenType();
        }
    }
}