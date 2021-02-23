using Sydy.Gambling.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public class GetTournamentResponse
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

            Matchs = matchs.Select(match => new Match(match));
            First = tournament.GetResults()?.ElementAtOrDefault(0).Key?.Name;
            Second = tournament.GetResults()?.ElementAtOrDefault(1).Key?.Name;
            Third = tournament.GetResults()?.ElementAtOrDefault(2).Key?.Name;
        }

        [JsonPropertyName("partidas")]
        public IEnumerable<Match>? Matchs { get; set; }
        [JsonPropertyName("campeao")]
        public string? First { get; set; }
        [JsonPropertyName("vice")]
        public string? Second { get; set; }
        [JsonPropertyName("terceiro")]
        public string? Third { get; set; }

        public class Match
        {
            public Match()
            {

            }

            public Match(IMatch match)
            {
                if (match is not { Results: ICollection<IMatchResult> _ })
                {
                    throw new ArgumentNullException(nameof(match));
                }

                DisplayName = match.GetDisplayName();
                DisplayScore = match.GetDisplayScore();
            }

            [JsonPropertyName("times")]
            public string? DisplayName { get; set; }
            [JsonPropertyName("resultado")]
            public string? DisplayScore { get; set; }
        }
    }
}
