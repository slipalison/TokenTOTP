using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TokenTOTP.API
{
    public class Startup : Infra.StartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }
    }
}