using Sydy.Gambling.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public class GetTeamStatsResponse
    {
        public GetTeamStatsResponse()
        {

        }

        public GetTeamStatsResponse(ITeam team, IEnumerable<IMatch>? matches)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            var matchResults = matches.SelectMany(match => match.Results!)
                .Where(results => results.Team?.Id == team.Id);

            Team = team as Team;
            Matches = matches.Select(match => new GetTournamentResponse.Match(match));
            Defeats = matches.Select(match => match.GetResultByTeam(team)).Where(keyValuePair => keyValuePair.Value == MatchResultKind.Defeat).Count();
            Draws = matches.Select(match => match.GetResultByTeam(team)).Where(keyValuePair => keyValuePair.Value == MatchResultKind.Draw).Count();
            Goals = matchResults.Select(results => results.Score).Sum();
            Victories = matches.Select(match => match.GetResultByTeam(team)).Where(keyValuePair => keyValuePair.Value == MatchResultKind.Victory).Count();
        }

        [JsonPropertyName("time")]
        public Team? Team { get; set; }
        [JsonPropertyName("partidas")]
        public IEnumerable<GetTournamentResponse.Match>? Matches { get; set; }
        [JsonPropertyName("gols")]
        public int Goals { get; set; }
        [JsonPropertyName("derrotas")]
        public int Defeats { get; set; }
        [JsonPropertyName("vitorias")]
        public int Victories { get; set; }
        [JsonPropertyName("empates")]
        public int Draws { get; set; }
    }
}
