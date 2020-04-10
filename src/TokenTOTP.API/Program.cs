using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace TokenTOTP.API
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, configuration) =>
                    {
                        configuration.AddEnvironmentVariables();

#pragma warning disable S125 // Sections of code should not be commented out
                        //if (!context.HostingEnvironment.IsDevelopment() && context.HostingEnvironment.EnvironmentName != "Test")
                        //{
                        //}
#pragma warning restore S125 // Sections of code should not be commented out
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
