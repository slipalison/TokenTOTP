namespace TokenTOTP.Shared.ViewModel
{
    public class CreateTokenCommand : TokenCommand
    {
        public CreateTokenCommand(string tokenType, int? timeToLive, string seed)
        {
            TokenType = tokenType;
            TimeToLive = timeToLive;
            Seed = seed;
        }
    }
}