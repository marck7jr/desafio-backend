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
            _httpClient = Tests.Program.Server.CreateClient();
        }

        public TestContext TestContext { get; set; }

        protected static async ValueTask<string> GetResponseContentAsync(HttpResponseMessage message) => $"{message}\n{await message.Content.ReadAsStringAsync()}";
    }
}
