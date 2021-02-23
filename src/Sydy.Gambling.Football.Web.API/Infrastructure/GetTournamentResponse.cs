using Sydy.Gambling.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public class GetTournamentResponse : IGetTournamentResponse
    {
        public GetTournamentResponse()
        {

        }

        public GetTournamentResponse(ITournament tournament)
        {
            if (tournament is not { Matches: ICollection<IMatch> matchs })
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            MatchResponses = matchs.Select(match => new GetTournamentMatchResponse(match));
            First = tournament.GetResults()?.ElementAt(0).Key.Name;
            Second = tournament.GetResults()?.ElementAt(1).Key.Name;
            Third = tournament.GetResults()?.ElementAt(2).Key.Name;
        }

        [JsonPropertyName("partidas")]
        public IEnumerable<GetTournamentMatchResponse>? MatchResponses { get; set; }
        [JsonPropertyName("campeao")]
        public string? First { get; set; }
        [JsonPropertyName("vice")]
        public string? Second { get; set; }
        [JsonPropertyName("terceiro")]
        public string? Third { get; set; }
    }
}
