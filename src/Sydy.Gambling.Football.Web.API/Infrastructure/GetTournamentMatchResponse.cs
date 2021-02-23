using Sydy.Gambling.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public class GetTournamentMatchResponse : IGetTournamentMatchResponse
    {
        public GetTournamentMatchResponse()
        {

        }

        public GetTournamentMatchResponse(IMatch match)
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
