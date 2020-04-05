using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace TokenTOTP.API
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, configuration) =>
                    {
                        configuration.AddEnvironmentVariables();
                        if (!context.HostingEnvironment.IsDevelopment() && context.HostingEnvironment.EnvironmentName != "Test")
                        {
                        }
                    })
                    .UseSerilog((hostingContext, cfg) =>
                    {
                        cfg.Enrich.FromLogContext();
                        cfg.Enrich.WithExceptionDetails();
                        cfg.Enrich.WithMachineName();
                        cfg.WriteTo.Console();
                    })
                    .UseStartup<Startup>();
                });
    }
}