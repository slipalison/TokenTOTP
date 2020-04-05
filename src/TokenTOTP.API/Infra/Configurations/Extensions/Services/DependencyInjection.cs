using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokenTOTP.API.Infra.Data.Repositories;
using TokenTOTP.API.Services;

namespace TokenTOTP.API.Infra.Configurations.Extensions.Services
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DefaultTimeToptOptions>(configuration.GetSection("DefaultTimeTopt"));

            // services
            services.AddScoped<OtpNetService>();
            services.AddScoped<ITokenTotpRepository, TokenTotpRepository>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}