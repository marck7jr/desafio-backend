using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IMatchService _matchService;

        public TournamentService(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async ValueTask<ITournament> GetTournamentAsync(CancellationToken cancellationToken = default)
        {
            var matches = await _matchService.GetMatchesAsync(cancellationToken).Cast<Match>().ToListAsync(cancellationToken);

            Tournament tournament = new()
            {
                Matches = matches
            };

            return tournament;
        }
    }
}
