using System;

namespace Sydy.Gambling.Football.Data.Models
{
    public interface ITeam : IEntity, IEquatable<ITeam>
    {
        public string? Name { get; set; }
    }
}
