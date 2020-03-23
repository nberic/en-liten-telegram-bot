
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EnLitenTelegramBot.Worker.Models.ApiTypes;

namespace EnLitenTelegramBot.Worker.Models
{
    public class GetUpdatesResponse
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("result")]
        public IEnumerable<Update> Result { get; set; }
    }
}