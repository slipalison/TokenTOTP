using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace TokenTOTP.Infra.Configurations.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MessageBrokerServicesExtension
    {
        public static void AddMessageBrokers(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RabbitMQ");

            //services.AddSendNotification(connectionString, lifetime: ServiceLifetime.Transient);
        }
    }
}