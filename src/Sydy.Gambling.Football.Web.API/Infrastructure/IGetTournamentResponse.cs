using System.Collections.Generic;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public interface IGetTournamentResponse
    {
        public IEnumerable<GetTournamentMatchResponse>? MatchResponses { get; }
        public string? First { get; set; }
        public string? Second { get; set; }
        public string? Third { get; set; }
    }
}
