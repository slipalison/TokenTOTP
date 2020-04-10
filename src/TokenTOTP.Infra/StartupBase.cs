using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TokenTOTP.Infra.Configurations.Extensions.Application;
using TokenTOTP.Infra.Configurations.Extensions.Services;
using TokenTOTP.Infra.Configurations.Middlewares;
using TokenTOTP.Infra.Swagger.Extensions;

namespace TokenTOTP.Infra
{
    public class StartupBase
    {
        public static readonly string DatabaseConectionName = "TokenDatabase";

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public StartupBase(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ILogger>(provider => provider.GetService<ILogger<StartupBase>>())
                .ConfigurationDatabase(_configuration)
                .ConfigurationBusinessService(_configuration)
                .AddApiConfigurations()
                .AddVersioning()
                .AddMediatR(typeof(Domain.ErrorMessages).Assembly, typeof(StartupBase).Assembly)
                .AddAutoMapper(typeof(StartupBase).Assembly)
                .AddSwaggerDocumentation()
                .HealthChecksConfiguration(_configuration)
                .GlobalizationConfiguration()
                .AddAuthenticationExtension(_configuration, _environment)
                .AddAuthorizationExtension(_configuration)
                .AddDependencyInjection(_configuration);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            app.DeveloperDependencies(_environment)
                .UseMiddleware<ExceptionMiddleware>()
                .UseRouting()
                .UseResponseCompression()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .HealthCheckConfiguration()
                .UseVersionedSwagger(versionProvider);
        }
    }
}
