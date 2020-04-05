using System;
using System.Threading.Tasks;
using TokenTOTP.Client.V1;
using Xunit;

namespace TokenTOTP.Tests.IntegrateTests
{
    [Collection(HttpClientCollection.Name)]
    public class ControllerTest : AbstractControllerTests
    {
        private readonly string _seed;

        public ControllerTest() : base(new HttpClientFixture().Client)
        {
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