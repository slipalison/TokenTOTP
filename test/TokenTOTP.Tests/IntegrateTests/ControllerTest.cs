using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TokenTOTP.Client.V1;
using TokenTOTP.Tests.StartServer;
using Xunit;

namespace TokenTOTP.Tests.IntegrateTests
{
    public class ControllerTest : StartTestServer
    {
        private readonly IIdentificationTokenService _identificationTokenService;
        private readonly string _seed;

        public ControllerTest()
        {
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory
                    .CreateClient(nameof(IdentificationTokenService))
                    .Returns(client);
            _identificationTokenService = new IdentificationTokenService(httpClientFactory);
            _seed = Guid.NewGuid().ToString();
        }

        [Fact(DisplayName = "Testa criação de token")]
        public async Task Create()
        {
            var a = await _identificationTokenService.CreateToken(_correlationId, "cel", _seed, 3700, _cancellationToken);
            Assert.True(a.IsSuccess);
        }
    }
}