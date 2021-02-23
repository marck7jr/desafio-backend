using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Data.Models
{
    public class Tournament : Entity, ITournament
    {
        private ICollection<Match>? matches = new List<Match>();

        [JsonPropertyName("partidas")]
        public virtual ICollection<Match>? Matches { get => GetValue(ref matches); set => SetValue(ref matches, value); }
        ICollection<IMatch>? ITournament.Matches { get => GetValue(ref matches)?.Cast<IMatch>().ToList(); set => SetValue(ref matches, (ICollection<Match>?)value); }
    }
}
