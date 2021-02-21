using Sydy.Gambling.Football.Data.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sydy.Gambling.Football.Web.API.Infrastructure
{
    public class GetTeamsResponse : IGetTeamsResponse
    {
        [JsonPropertyName("pagina")]
        public int Page { get; set; }
        [JsonPropertyName("tamanho")]
        public int Size { get; set; }
        [JsonPropertyName("qtdPagina")]
        public int Count { get; set; }
        [JsonPropertyName("itens")]
        public IEnumerable<Team> Teams { get; set; }
    }
}
