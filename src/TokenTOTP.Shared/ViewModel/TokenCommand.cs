namespace TokenTOTP.Shared.ViewModel
{
    public abstract class TokenCommand
    {
        public string Seed { get; protected set; }

        public string TokenType { get; protected set; }

        public int? TimeToLive { get; protected set; }
    }
}