using System.Collections.Generic;
using System.Linq;

namespace Sydy.Gambling.Football.Data.Models
{
    public interface IMatch : IEntity
    {
        public string GetDisplayName() => $"{this[0]?.Team?.Name} {this[0]?.Score} x {this[1]?.Score} {this[1]?.Team?.Name}";
        public string GetDisplayScore() => $"{this[0]?.Score} x {this[1]?.Score}";
        public IEnumerable<KeyValuePair<ITeam, MatchResultKind>> GetResultsPerTeam()
        {
            if (Results?.Count == 2)
            {
                yield return new KeyValuePair<ITeam, MatchResultKind>(this[0]!.Team!, this[0]! + this[1]!);
                yield return new KeyValuePair<ITeam, MatchResultKind>(this[1]!.Team!, this[1]! + this[0]!);
            }

            yield break;
        }

        public KeyValuePair<ITeam, MatchResultKind> GetResultByTeam(ITeam team) => GetResultsPerTeam().Where(keyValuePair => keyValuePair.Key.Id == team.Id).First();

        public ICollection<IMatchResult>? Results { get; set; }
        public IMatchResult? this[int index] => Results?.ElementAtOrDefault(index);
    }
}
