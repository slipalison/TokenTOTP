using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;

namespace TokenTOTP.API.Infra.Configurations.Extensions.Application
{
    public static class HealthCheckConfigurationExtension
    {
        public static IApplicationBuilder HealthCheckConfiguration(this IApplicationBuilder app)
            => app
            .UseHealthChecks(new PathString("/liveness"), new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
            .UseHealthChecks(new PathString("/hc"), new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
    }
}