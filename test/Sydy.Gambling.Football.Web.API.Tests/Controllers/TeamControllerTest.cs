using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Data.Models;
using Sydy.Gambling.Football.Web.API.Infrastructure;
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
        [DataRow("Corinthians")]
        [DataRow("São Paulo")]
        [DataRow("Cuiabá")]
        [DataRow("Jangada FC")]
        [DataRow("Luverdense")]
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
                var errorMessage = await GetErrorMessageFromHttpResponseMessageAsync(response);

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
            var errorMessage = await GetErrorMessageFromHttpResponseMessageAsync(response);

            Assert.IsFalse(response.IsSuccessStatusCode);

            TestContext.WriteLine(errorMessage);
        }

        [TestMethod]
        [DataRow(1, "Sydy Editado")]
        public async Task PutAsync_IsSucessStatusCode(int id, string teamName)
        {
            var body = new
            {
                nome = teamName
            };

            var putResponse = await _httpClient.PutAsJsonAsync($"{RequestUri}/{id}", body);
            var getResponse = await _httpClient.GetAsync($"{RequestUri}/{id}");
            var team = await getResponse.Content.ReadFromJsonAsync<Team>();

            Assert.IsTrue(putResponse.IsSuccessStatusCode, await GetErrorMessageFromHttpResponseMessageAsync(putResponse));
            Assert.IsTrue(getResponse.IsSuccessStatusCode, await GetErrorMessageFromHttpResponseMessageAsync(getResponse));
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
        public async Task GetAsync_IsNotNull()
        {
            var response = await _httpClient.GetAsync(RequestUri);
            var getTeamsResponse = await response.Content.ReadFromJsonAsync<GetTeamsResponse>();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(getTeamsResponse);
            Assert.IsTrue(getTeamsResponse.Count > 0);

            TestContext.WriteLine(JsonSerializer.Serialize(getTeamsResponse));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
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
