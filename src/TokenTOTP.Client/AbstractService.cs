using System.Net.Http;
using Flurl.Http;

namespace TokenTOTP.Client
{
    public abstract class AbstractService
    {
        protected IFlurlClient _client { get; }

        protected AbstractService(IHttpClientFactory httpClientFactory, string clientName)
        {
            _client = new FlurlClient(httpClientFactory.CreateClient(clientName));
        }

        protected IFlurlRequest FlurlRequest(string uri, string correlationId)
             => $"{uri}".WithClient(_client).WithHeader("X-Correlation-ID", correlationId).AllowAnyHttpStatus();

        protected IFlurlRequest FlurlRequest(string uri)
            => $"{uri}".WithClient(_client).AllowAnyHttpStatus();
    }
}
