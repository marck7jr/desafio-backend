using Sydy.Gambling.Football.MVVM;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Data
{
    public abstract class Entity : ObservableObject, IEntity
    {
        private int id;
        private DateTime createdAt = DateTime.UtcNow;
        private DateTime? updatedAt;

        public Entity()
        {
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(UpdatedAt))
                {
                    UpdatedAt = DateTime.UtcNow;
                }
            };
        }

        [Key]
        [JsonPropertyName("id")]
        public int Id { get => GetValue(ref id); set => SetValue(ref id, value); }
        [JsonPropertyName("criadoEm")]
        public DateTime CreatedAt { get => GetValue(ref createdAt); set => SetValue(ref createdAt, value); }
        [JsonPropertyName("atualizadoEm")]
        public DateTime? UpdatedAt { get => GetValue(ref updatedAt); set => SetValue(ref updatedAt, value); }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(CreatedAt), CreatedAt);
            info.AddValue(nameof(UpdatedAt), UpdatedAt);

            base.GetObjectData(info, context);
        }
    }
}
