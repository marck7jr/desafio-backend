using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sydy.Gambling.Football.Services
{
    public class TeamsService : ITeamsService
    {
        private ApplicationDbContext _context;

        public TeamsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAsyncEnumerable<ITeam> GetTeamsAsync(int page = 1, int size = 10) => _context.Teams
            .AsAsyncQueryable()
            .Skip(--page * size)
            .Take(size);
    }
}
