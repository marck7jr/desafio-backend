using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Web.API.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API.Tests.Controllers
{
    [TestClass]
    public class TournamentControllerTest : ControllerTest
    {
        private const string RequestUri = "api/campeonato";

        [TestMethod]
        public async Task GetAsync_IsNotNull()
        {
            var response = await _httpClient.GetAsync(RequestUri);

            if (response is { StatusCode: HttpStatusCode.NoContent })
            {
                Assert.Inconclusive();
            }

            var getTournamentResponse = await response.Content.ReadFromJsonAsync<GetTournamentResponse>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(getTournamentResponse);
            Assert.IsTrue(getTournamentResponse.Matchs.Any());
            Assert.IsTrue(!string.IsNullOrEmpty(getTournamentResponse.First));
            Assert.IsTrue(!string.IsNullOrEmpty(getTournamentResponse.Second));
            Assert.IsTrue(!string.IsNullOrEmpty(getTournamentResponse.Third));

            TestContext.WriteLine(JsonSerializer.Serialize(getTournamentResponse, new() { WriteIndented = true }));
        }
    }
}
