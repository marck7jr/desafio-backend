using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sydy.Gambling.Football.Services
{
    public class TeamsService : ITeamsService
    {
        private ApplicationDbContext _applicationDbContext;

        public TeamsService(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async IAsyncEnumerable<Team> GetTeamsAsync(int page = 1, int size = 10, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var count = await _applicationDbContext.Teams.CountAsync();
            var skip = --page * size;

            if (count >= skip)
            {
                var teams = _applicationDbContext.Teams
                    .AsAsyncQueryable()
                    .Skip(skip)
                    .Take(size);

                await foreach (var team in teams)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    yield return team;
                }
            }

            yield break;
        }
    }
}
