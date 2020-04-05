using Flurl.Http;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading;
using TokenTOTP.Client.V1;

namespace TokenTOTP.Tests.IntegrateTests
{
    public class AbstractControllerTests
    {
        protected readonly IFlurlClient _flurlClient;
        protected readonly CancellationToken _cancellationToken;
        protected readonly string _correlationId;

        protected readonly IIdentificationTokenService _identificationTokenService;

        public AbstractControllerTests(IFlurlClient flurlClient)
        {
            _flurlClient = flurlClient;

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory
                .CreateClient(nameof(IdentificationTokenService))
                .Returns(_flurlClient.HttpClient);
            _identificationTokenService = new IdentificationTokenService(httpClientFactory);
            _cancellationToken = CancellationToken.None;
            _correlationId = Guid.NewGuid().ToString();
        }
    }
}