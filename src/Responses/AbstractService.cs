using Flurl.Http;
using System.Net.Http;

namespace Responses
{
    public abstract class AbstractService
    {
        protected IFlurlClient _client { get; }

        protected AbstractService(IHttpClientFactory httpClientFactory, string ClientName)
        {
            _client = new FlurlClient(httpClientFactory.CreateClient(ClientName));
        }

        protected IFlurlRequest FlurlRequest(string uri, string correlationId)
             => $"{uri}".WithClient(_client).WithHeader("X-Correlation-ID", correlationId).AllowAnyHttpStatus();

        protected IFlurlRequest FlurlRequest(string uri)
            => $"{uri}".WithClient(_client).AllowAnyHttpStatus();
    }
}