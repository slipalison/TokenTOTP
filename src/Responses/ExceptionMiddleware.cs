using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Responses
{
    public class ExceptionMiddleware
    {
        public RequestDelegate Next { get; }

        public ILogger Logger { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch(Exception e)
            {
                Logger.LogError(e, "An internal error occurred.");
                throw;
            }
        }
    }
}
