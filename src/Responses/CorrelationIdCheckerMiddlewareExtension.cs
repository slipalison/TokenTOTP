using Microsoft.AspNetCore.Builder;

namespace Responses
{
    public static class CorrelationIdCheckerMiddlewareExtension
    {
        public static IApplicationBuilder UseCorrelationIdChecker(this IApplicationBuilder builder) => builder.UseMiddleware<CorrelationIdCheckerMiddleware>();
    }
}
