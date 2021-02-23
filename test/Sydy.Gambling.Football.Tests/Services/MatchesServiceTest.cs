using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Services
{
    [TestClass]
    public class MatchesServiceTest
    {
        private static ApplicationDbContext _applicationDbContext;
        private static IMatchService _matchesService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _applicationDbContext = Tests.Program.Services.GetRequiredService<ApplicationDbContext>();
            _matchesService = Tests.Program.Services.GetRequiredService<IMatchService>();
        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task GetMatchesAsync_IsNotEmpty()
        {
            var count = await _applicationDbContext.Teams.CountAsync();
            var expectedMatchesCount = Enumerable.Range(0, count).Sum();

            var matches = await _matchesService.GetMatchesAsync().Cast<Match>().ToListAsync();

            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Any());
            Assert.AreEqual(expectedMatchesCount, matches.Count);

            TestContext.WriteLine(JsonSerializer.Serialize(matches, new() { WriteIndented = true }));
        }
    }
}
