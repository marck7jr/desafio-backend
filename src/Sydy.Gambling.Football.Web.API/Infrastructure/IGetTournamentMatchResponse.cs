using Sydy.Gambling.Football.Data.Models;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public interface IGetTournamentMatchResponse
    {
        public string? DisplayName { get; set; }
        public string? DisplayScore { get; set; }
    }
}
