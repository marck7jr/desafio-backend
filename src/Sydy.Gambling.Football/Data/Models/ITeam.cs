using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Data.Models
{
    public interface ITeam : IEntity
    {
        public string? Name { get; set; }
    }
}
