using System;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TokenTOTP.Tests
{
    public class HttpClientFixture : IDisposable
    {
        public TestServer Server { get; }
        public IFlurlClient Client { get; }

        public HttpClientFixture()
        {
            Server = new TestServer(new WebHostBuilder()
                            .UseEnvironment("Test")
                            .UseStartup<StartupTest>());

            Client = new FlurlClient(Server.CreateClient());
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}