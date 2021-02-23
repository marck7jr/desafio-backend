using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;
using System.Threading;

namespace Sydy.Gambling.Football.Services
{
    public interface IMatchService
    {
        public IAsyncEnumerable<IMatch> GetMatchesAsync(CancellationToken cancellationToken = default);
    }
}
