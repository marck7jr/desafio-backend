using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Web.API.Tests.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API
{
    [TestClass]
    public abstract class ControllerTest
    {
        protected HttpClient _httpClient;

        [TestInitialize]
        public virtual void Setup()
        {
            var builder = new WebHostBuilder()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile("appSettings.json", true, true);
                    configuration.AddJsonFile($"appSettings.{context.HostingEnvironment}.json", true, true);
                })
                .ConfigureServices((context, services) =>
                {
                    var serviceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(serviceDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(typeof(ControllerTest).FullName);
                    });

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

            var server = new TestServer(builder);

            _httpClient = server.CreateClient();
        }

        public TestContext TestContext { get; set; }

        protected static async ValueTask<string> GetErrorMessageFromHttpResponseMessageAsync(HttpResponseMessage message) => $"{message}\n{await message.Content.ReadAsStringAsync()}";
    }
}
