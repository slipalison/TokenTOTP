using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Responses
{
    public class CurrentCorrelationId : ICurrentCorrelationId
    {
        private const string CORRELATION_ID_KEY = "x-correlation-id";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentCorrelationId(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public string CorrelationId => _httpContextAccessor
                .HttpContext
                .Request
                .Headers
                .First(x => x.Key.ToLower() == CORRELATION_ID_KEY).Value;
    }
}