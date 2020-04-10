using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace TokenTOTP.Infra.Configurations.Extensions.Services
{
    public static class GlobalizationConfigurationExtension
    {
        public static IServiceCollection GlobalizationConfiguration(this IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            return services;
        }
    }
}
