using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Responses;
using TokenTOTP.API.Infra.Configurations.Extensions.Application;
using TokenTOTP.API.Infra.Configurations.Extensions.Services;
using TokenTOTP.API.Infra.Swagger.Extensions;

namespace TokenTOTP.API
{
    public class Startup
    {
        public static readonly string DatabaseConectionName = "TokenDatabase";

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ILogger>(provider => provider.GetService<ILogger<Startup>>())
                .ConfigurationDatabase(_configuration)
                .ConfigurationBusinessService(_configuration)
                .AddApiConfigurations()
                .AddVersioning()
                .AddMediatR(typeof(Startup).Assembly)
                .AddAutoMapper(typeof(Startup).Assembly)
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