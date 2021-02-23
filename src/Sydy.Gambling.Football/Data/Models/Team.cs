using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Data.Models
{
    public class Team : Entity, ITeam, IEquatable<Team>
    {
        private string? name;

        [JsonPropertyName("nome")]
        [StringLength(32, MinimumLength = 3)]
        public string? Name { get => GetValue(ref name); set => SetValue(ref name, value); }

        public bool Equals(ITeam? other)
        {
            return other is not null && Id == other.Id;
        }

        public bool Equals(Team? other)
        {
            return Equals(other as ITeam);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Team);
        }

        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);

            base.GetObjectData(info, context);
        }

    }
}
