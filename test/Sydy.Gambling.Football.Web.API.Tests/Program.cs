using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Tests.Extensions;
using System;
using System.Diagnostics;

namespace Sydy.Gambling.Football.Web.API.Tests
{
    [TestClass]
    public class Program
    {
        public static TestServer Server { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            var builder = new WebHostBuilder()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile("appSettings.json", true, true);
                    configuration.AddJsonFile($"appSettings.{context.HostingEnvironment}.json", true, true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddInMemoryDbContext<ApplicationDbContext>();

                    using var serviceScope = services.BuildServiceProvider().CreateScope();
                    var serviceProvider = serviceScope.ServiceProvider;
                    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

                    applicationDbContext.Database.EnsureCreated();

                    try
                    {
                        applicationDbContext.Teams.Seed();
                        applicationDbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                })
                .UseStartup<Startup>();

            Server = new TestServer(builder);
        }
    }
}
