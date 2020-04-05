using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokenTOTP.Domain.Repositories;
using TokenTOTP.Domain.Services;
using TokenTOTP.Infra.Data.Repositories;

namespace TokenTOTP.Infra.Configurations.Extensions.Services
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