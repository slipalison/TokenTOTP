using Microsoft.Extensions.DependencyInjection;

namespace Responses
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCurrentCorrelationId(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentCorrelationId, CurrentCorrelationId>();

            return services;
        }
    }
}