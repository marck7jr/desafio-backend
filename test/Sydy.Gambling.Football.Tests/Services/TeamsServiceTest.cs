using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Services
{
    [TestClass]
    public class TeamsServiceTest
    {
        private static ITeamsService _teamsService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _teamsService = Tests.Program.Services.GetRequiredService<ITeamsService>();
        }

        [TestMethod]
        [DataRow(1, 10)]
        [DataRow(2, 3)]
        [DataRow(7, 1)]
        public async Task GetTeamsAsync_IsNotEmpty(int page, int size)
        {
            var teams = await _teamsService.GetTeamsAsync(page, size).ToListAsync();
            var id = (--page * size) + 1;

            Assert.IsNotNull(teams);
            Assert.IsTrue(teams.Any());
            Assert.AreEqual(teams.First().Id, id);
        }

        [TestMethod]
        [DataRow(4, 3)]
        [DataRow(100, 50)]
        public async Task GetTeamsAsync_IsEmpty(int page, int size)
        {
            var teams = await _teamsService.GetTeamsAsync(page, size).ToListAsync();

            Assert.IsNotNull(teams);
            Assert.IsFalse(teams.Any());
        }
    }
}
