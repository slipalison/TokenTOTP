using System.Threading.Tasks;
using Core.Customer.Client.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TokenTOTP.API.Infra.Configurations.Extensions.Services
{
    public static class ServicesConfigurationsExtension
    {
        public static IServiceCollection ConfigurationBusinessService(this IServiceCollection services, IConfiguration configuration)
        {
            if(configuration["environment"] == "Test")
                return services;

            services.AddHttpClient(nameof(CustomerApiClient), config =>
            {
                config.BaseAddress = new System.Uri(configuration.GetSection("Services:CoreCustomerNew").GetValue<string>("Url"));
            });

            services.AddScoped<ICustomerApiClient, CustomerApiClient>();
            return services;
        }

        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            var authority = config["Auth:Authority"];
            if(hostingEnvironment.EnvironmentName != "Production" && config["environment"] != "Test")
                authority = "https://login.acessobank-stg.com.br";

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