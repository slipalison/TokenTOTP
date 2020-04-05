using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TokenTOTP.API.Infra.Configurations.Extensions.Services
{
    public static class HealthChecksConfigurationExtension
    {
        public static IServiceCollection HealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddMySql(configuration.GetConnectionString(Startup.DatabaseConectionName));

            return services;
        }
    }
}