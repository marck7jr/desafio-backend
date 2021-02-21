using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;

namespace Sydy.Gambling.Football.Services
{
    public interface ITeamsService
    {
        public IAsyncEnumerable<ITeam> GetTeamsAsync(int page = 1, int size = 10);
    }
}
