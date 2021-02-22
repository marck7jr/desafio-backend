using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Data.Models;
using Sydy.Gambling.Football.Web.API.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API.Controllers
{
    [TestClass]
    public class TeamControllerTest : ControllerTest
    {
        private const string RequestUri = "api/time";

        [TestMethod]
        [DataRow("Sydy Esports")]
        public async Task PostAsync_IsConflict_Or_IsSuccessStatusCode(string teamName)
        {
            Team team = new()
            {
                Name = teamName
            };

            var response = await _httpClient.PostAsJsonAsync(RequestUri, team);

            if (response.IsSuccessStatusCode)
            {
                Team _team = await response.Content.ReadFromJsonAsync<Team>();
                Assert.AreEqual(team.Name, _team.Name);
            }
            else
            {
                var errorMessage = await GetErrorMessageAsync(response);

                if (response is { StatusCode: HttpStatusCode.Conflict })
                {
                    Assert.Inconclusive(errorMessage);
                }
                else
                {
                    Assert.Fail(errorMessage);
                }
            }
        }


        [TestMethod]
        [DataRow("")]
        [DataRow("S")]
        [DataRow("Sy")]
        public async Task PostAsync_ModelIsNotValid(string teamName)
        {
            Team team = new()
            {
                Name = teamName
            };

            var response = await _httpClient.PostAsJsonAsync(RequestUri, team);
            var errorMessage = await GetErrorMessageAsync(response);

            Assert.IsFalse(response.IsSuccessStatusCode);

            TestContext.WriteLine(errorMessage);
        }

        [TestMethod]
        [DataRow(2, "Sydy Editado")]
        public async Task PutAsync_IsSucessStatusCode(int id, string teamName)
        {
            var body = new
            {
                nome = teamName
            };

            var putResponse = await _httpClient.PutAsJsonAsync($"{RequestUri}/{id}", body);
            var getResponse = await _httpClient.GetAsync($"{RequestUri}/{id}");
            var team = await getResponse.Content.ReadFromJsonAsync<Team>();

            Assert.IsTrue(putResponse.IsSuccessStatusCode, await GetErrorMessageAsync(putResponse));
            Assert.IsTrue(getResponse.IsSuccessStatusCode, await GetErrorMessageAsync(getResponse));
            Assert.IsNotNull(team);
            Assert.AreEqual(teamName, team.Name);

            TestContext.WriteLine(JsonSerializer.Serialize(team));
        }

        [TestMethod]
        [DataRow(1)]
        public async Task DeleteAsync_IsSucessStatusCode(int id)
        {
            var deleteResponse = await _httpClient.DeleteAsync($"{RequestUri}/{id}");
            var getResponse = await _httpClient.GetAsync($"{RequestUri}/{id}");

            Assert.IsTrue(deleteResponse.IsSuccessStatusCode);
            Assert.IsTrue(getResponse is { StatusCode: HttpStatusCode.NotFound });
        }

        [TestMethod]
        [DataRow(2, 3)]
        public async Task GetAsync_IsSucessStatusCode(int page, int size)
        {
            var response = await _httpClient.GetAsync($"{RequestUri}?pagina={page}&tamanhoPagina={size}");
            var getTeamsResponse = await response.Content.ReadFromJsonAsync<GetTeamsResponse>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(getTeamsResponse);
            Assert.IsTrue(getTeamsResponse.Teams.Any());

            TestContext.WriteLine(JsonSerializer.Serialize(getTeamsResponse));
        }

        [TestMethod]
        [DataRow(53, 1000)]
        public async Task GetAsync_IsNotFoundStatusCode(int page, int size)
        {
            var response = await _httpClient.GetAsync($"{RequestUri}?pagina={page}&tamanhoPagina={size}");

            Assert.IsTrue(response is { StatusCode: HttpStatusCode.NotFound });
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public async Task GetTeamAsync_IsNotNull(int id)
        {
            var response = await _httpClient.GetAsync($"{RequestUri}/{id}");
            var team = await response.Content.ReadFromJsonAsync<Team>();

            Assert.IsNotNull(response.IsSuccessStatusCode);
            Assert.IsNotNull(team);
            Assert.AreEqual(team.Id, id);

            TestContext.WriteLine(JsonSerializer.Serialize(team));
        }
    }
}
