namespace TokenTOTP.Shared.ViewModel
{
    /// <summary>
    /// Dados para a validação do token
    /// </summary>
    public class ValidateTokenRequest
    {
        /// <summary>
        /// Token para ser buscado
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Indica se o token será deletado após a validação
        /// </summary>
        public string Hash { get; set; }
    }
}