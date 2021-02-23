using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public interface IGetTeamsResponse
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public IEnumerable<Team>? Teams { get; set; }
    }
}
