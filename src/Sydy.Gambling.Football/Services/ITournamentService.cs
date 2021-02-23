using Sydy.Gambling.Football.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Services
{
    public interface ITournamentService
    {
        public ValueTask<ITournament> GetTournamentAsync(CancellationToken cancellationToken = default);
    }
}
