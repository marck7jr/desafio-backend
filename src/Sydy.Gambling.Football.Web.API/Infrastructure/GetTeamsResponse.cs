using Sydy.Gambling.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public class GetTeamsResponse : IGetTeamsResponse
    {
        public GetTeamsResponse()
        {

        }

        public GetTeamsResponse(double page, double count, double size, IEnumerable<ITeam> teams)
        {
            Count = (int)Math.Round(count / size, MidpointRounding.AwayFromZero);
            Page = (int)page;
            Size = (int)size;
            Teams = teams.Cast<Team>();
        }

        [JsonPropertyName("pagina")]
        public int Page { get; set; }
        [JsonPropertyName("tamanho")]
        public int Size { get; set; }
        [JsonPropertyName("qtdPagina")]
        public int Count { get; set; }
        [JsonPropertyName("itens")]
        public IEnumerable<Team>? Teams { get; set; }
    }
}
