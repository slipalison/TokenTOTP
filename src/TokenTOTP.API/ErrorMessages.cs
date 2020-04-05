namespace TokenTOTP.API
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ErrorMessages
    {
        public static readonly (string code, string message) InvalidCorrelationID = ("000", "Invalid Correlation-ID");
        public static readonly (string code, string message) BadRequest = ("001", "Invalid Payload");
    }
}