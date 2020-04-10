using System;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace TokenTOTP.Tests.StartServer
{
    public class StartTestServer
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly IHost _host;
        protected readonly HttpClient client;
        protected readonly string _correlationId;
        protected readonly CancellationToken _cancellationToken;

        public StartTestServer()
        {
            _hostBuilder = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer();
                webHost.UseStartup<StartUpTest>();
            });

            _host = _hostBuilder.Start();

            client = _host.GetTestClient();
            _correlationId = Guid.NewGuid().ToString();
            _cancellationToken = CancellationToken.None;
        }
    }
}
