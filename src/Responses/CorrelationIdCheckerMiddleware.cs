using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Responses
{
    public class CorrelationIdCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdCheckerMiddleware> _logger;

        public CorrelationIdCheckerMiddleware(RequestDelegate next, ILogger<CorrelationIdCheckerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api"))
            {
                var hasCorrelationId = httpContext.Request.Headers.TryGetValue("x-correlation-id", out _);

                if (hasCorrelationId) await _next(httpContext);
                else
                {
                    _logger.LogWarning("Request without x-correlation-id");
                    await HandleResponseAsync(httpContext);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }

        private static async Task HandleResponseAsync(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var error = new Error("001", "Request without correlation id") { ApplicationName = Assembly.GetCallingAssembly().GetName().Name };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(Result.Fail(error)));
        }
    }
}