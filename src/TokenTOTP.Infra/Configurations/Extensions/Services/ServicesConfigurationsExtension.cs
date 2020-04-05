using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace TokenTOTP.Infra.Configurations.Extensions.Services
{
    public static class ServicesConfigurationsExtension
    {
        public static IServiceCollection ConfigurationBusinessService(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration["environment"] == "Test")
                return services;
            return services;
        }

        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            var authority = config["Auth:Authority"];
            if (hostingEnvironment.EnvironmentName != "Production" && config["environment"] != "Test")
            {
                // authority = "";
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = config["Auth:Audience"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c => Task.CompletedTask
                };
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services, IConfiguration config)
        {
            return services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim("scope", config["Auth:Scope"])
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }
    }
}