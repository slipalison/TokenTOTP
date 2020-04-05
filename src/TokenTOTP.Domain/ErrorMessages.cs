namespace TokenTOTP.Domain
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ErrorMessages
    {
        public static readonly (string code, string message) InvalidCorrelationID = ("000", "Invalid Correlation-ID");
        public static readonly (string code, string message) InputInvalid = ("001", "Invalid Payload");
        public static readonly (string code, string message) ExpiredToken = ("002", "Expired Token");
        public static readonly (string code, string message) TokenNotFound = ("003", "Token Not Found");
        public static readonly (string code, string message) CorrelationIdNotFound = ("004", "CorrelationId Not Found");
    }
}