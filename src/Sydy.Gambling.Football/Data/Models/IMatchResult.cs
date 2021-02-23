using System;
using System.Diagnostics.CodeAnalysis;

namespace Sydy.Gambling.Football.Data.Models
{
    public enum MatchResultKind
    {
        Defeat = 0,
        Draw = 1,
        Victory = 3,
    }

    public interface IMatchResult : IEntity
    {
        public ITeam? Team { get; set; }
        public int Score { get; set; }

        public static MatchResultKind operator +([NotNull] IMatchResult matchResult, [NotNull] IMatchResult other)
        {
            return matchResult.Score switch
            {
                int integer when integer < other.Score => MatchResultKind.Defeat,
                int integer when integer == other.Score => MatchResultKind.Draw,
                int integer when integer > other.Score => MatchResultKind.Victory,
                _ => throw new NotSupportedException()
            };
        }
    }
}
