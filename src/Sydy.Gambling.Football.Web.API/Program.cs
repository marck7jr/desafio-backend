using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sydy.Gambling.Football.Data;
using System;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API
{
    public class Program
    {
        public static bool IsContainer => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (IsContainer)
            {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                try
                {
                    var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();

                    if (applicationDbContext.Database.IsSqlServer())
                    {
                        await applicationDbContext.Database.MigrateAsync();
                    }
                }
                catch (Exception exception)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(exception, "Ocorreu um erro durante a migração do banco de dados.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
