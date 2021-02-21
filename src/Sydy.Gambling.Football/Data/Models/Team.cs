using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Data.Models
{
    public class Team : Entity, ITeam
    {
        private string? name;

        [JsonPropertyName("nome")]
        [StringLength(32, MinimumLength = 3)]
        public string? Name { get => GetValue(ref name); set => SetValue(ref name, value); }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);

            base.GetObjectData(info, context);
        }
    }
}
