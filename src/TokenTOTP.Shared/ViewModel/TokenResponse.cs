namespace TokenTOTP.Shared.ViewModel
{
    /// <summary>
    /// Dados do Token Criado
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Hash criado
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Token criado
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Tempo, em segundos, que este token será valido
        /// </summary>
        public long TimeToLive { get; set; }
    }
}