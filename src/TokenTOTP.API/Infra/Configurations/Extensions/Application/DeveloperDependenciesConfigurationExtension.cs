using TokenTOTP.API.Infra.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TokenTOTP.API.Infra.Configurations.Extensions.Application
{
    public static class DeveloperDependenciesConfigurationExtension
    {
        public static IApplicationBuilder DeveloperDependencies(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                    serviceScope.ServiceProvider.GetRequiredService<TokenContext>().Database.Migrate();
            }

            return app;
        }
    }
}