using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokenTOTP.API;
using TokenTOTP.Infra.Data.Contexts;

namespace TokenTOTP.Tests.StartServer
{
    public class StartUpTest : Startup
    {
        public StartUpTest(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
            configuration["DefaultTimeTopt:Range"] = "60";
            configuration["DefaultTimeTopt:Size"] = "6";
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<TokenContext>();
                dbContext.Database.EnsureCreated();
            }
            base.Configure(app, env, versionProvider);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(Assembly.Load(new AssemblyName("TokenTOTP.API")));
            services.AddEntityFrameworkSqlite();
            var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            inMemorySqlite.Open();

            services.AddDbContext<TokenContext>(x =>
            {
                x.UseSqlite(inMemorySqlite);
            });
            base.ConfigureServices(services);
        }
    }
}
