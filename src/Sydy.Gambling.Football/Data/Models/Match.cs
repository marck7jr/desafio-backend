using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Data.Models
{
    [Serializable]
    public class Match : Entity, IMatch
    {
        private ICollection<MatchResult>? results = new List<MatchResult>(2);

        [JsonPropertyName("resultados")]
        public virtual ICollection<MatchResult>? Results { get => GetValue(ref results); set => SetValue(ref results, value); }
        ICollection<IMatchResult>? IMatch.Results { get => GetValue(ref results)?.Cast<IMatchResult>().ToList(); set => SetValue(ref results, (ICollection<MatchResult>?)value); }

        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Results), Results);

            base.GetObjectData(info, context);
        }

        public override string ToString() => ((IMatch)this).GetDisplayName();
    }
}
