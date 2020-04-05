namespace TokenTOTP.Shared.ViewModel
{
    public class CreateTokenCommand : ITokenCommand
    {
        public CreateTokenCommand(string tokenType, int? timeToLive, string seed)
        {
            TokenType = tokenType;
            TimeToLive = timeToLive;
            Seed = seed;
        }

        public string Seed { get; set; }
        public string TokenType { get; set; }
        public int? TimeToLive { get; set; }
    }
}