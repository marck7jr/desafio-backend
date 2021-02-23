using System.Collections.Generic;
using System.Linq;

namespace Sydy.Gambling.Football.Data.Models
{
    public interface ITournament
    {
        public ICollection<IMatch>? Matches { get; set; }
        public IEnumerable<KeyValuePair<ITeam, int>>? GetResults()
        {
            var keyValuePairs = Matches?.SelectMany(match => match.GetResultsPerTeam())
                .GroupBy(keyValuePair => keyValuePair.Key)
                .Select(group => new KeyValuePair<ITeam, int>(group.Key, group.Sum(keyValuePair => (int)keyValuePair.Value)))
                .OrderByDescending(keyValuePair => keyValuePair.Value);

            return keyValuePairs;
        }
    }
}
