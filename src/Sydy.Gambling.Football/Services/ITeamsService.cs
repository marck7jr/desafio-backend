using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;
using System.Threading;

namespace Sydy.Gambling.Football.Services
{
    public interface ITeamsService
    {
        public IAsyncEnumerable<Team> GetTeamsAsync(int page = 1, int size = 10, CancellationToken cancellationToken = default);
    }
}
