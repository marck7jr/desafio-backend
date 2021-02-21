using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                .UseStartup<Startup>();

            var server = new TestServer(builder);

            _httpClient = server.CreateClient();
        }

        public TestContext TestContext { get; set; }

        protected static async ValueTask<string> GetErrorMessageFromHttpResponseMessageAsync(HttpResponseMessage message) => $"{message}\n{await message.Content.ReadAsStringAsync()}";
    }
}
