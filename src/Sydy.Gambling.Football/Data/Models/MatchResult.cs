using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Data.Models
{
    [Serializable]
    public class MatchResult : Entity, IMatchResult
    {
        private ITeam? team;
        private int score;
        private Match? match;

        [JsonPropertyName("partida")]
        public Match? Match { get => GetValue(ref match); set => SetValue(ref match, value); }
        [JsonPropertyName("time")]
        public Team? Team { get => GetValue(ref team) as Team; set => SetValue(ref team, value); }
        ITeam? IMatchResult.Team { get => GetValue(ref team); set => SetValue(ref team, value); }
        [JsonPropertyName("gols")]
        public int Score { get => GetValue(ref score); set => SetValue(ref score, value); }

        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Team), Team);
            info.AddValue(nameof(Score), Score);

            base.GetObjectData(info, context);
        }
    }
}
