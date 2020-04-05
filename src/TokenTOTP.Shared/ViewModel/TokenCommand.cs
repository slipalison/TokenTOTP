namespace TokenTOTP.Shared.ViewModel
{
    public interface ITokenCommand
    {
        string Seed { get; }

        string TokenType { get; }

        int? TimeToLive { get; }
    }
}