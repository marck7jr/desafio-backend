using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Services
{
    [TestClass]
    public class TournamentServiceTest
    {
        private static ITournamentService _tournamentService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _tournamentService = Tests.Program.Services.GetRequiredService<ITournamentService>();
        }

        public TestContext TestContext { get; set; }


        [TestMethod]
        public async Task GetTournamentAsync_IsNotNull()
        {
            var tournament = await _tournamentService.GetTournamentAsync();

            Assert.IsNotNull(tournament);
            Assert.IsTrue(tournament.Matches.Any());

            TestContext.WriteLine(JsonSerializer.Serialize(tournament, new() { WriteIndented = true }));
        }
    }
}
