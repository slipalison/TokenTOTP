using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using TokenTOTP.API.Infra.Data.Contexts;

namespace TokenTOTP.API.Infra.Configurations.Extensions.Services
{
    public static class DatabaseConfigurationExtesion
    {
        public static IServiceCollection ConfigurationDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration["environment"] != "Test")
            {
                services
                        .AddEntityFrameworkMySql()
                        .AddDbContextPool<TokenContext>(options =>
                        {
                            options.UseMySql(configuration.GetConnectionString(Startup.DatabaseConectionName), mySqlOptions =>
                            {
                                mySqlOptions.ServerVersion(new Version(8, 0, 0), ServerType.MySql);
                            });
                        });
            }
            return services;
        }
    }
}