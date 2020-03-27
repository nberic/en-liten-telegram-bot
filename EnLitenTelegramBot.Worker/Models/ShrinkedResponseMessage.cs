using System.Text.Json.Serialization;
using EnLitenTelegramBot.Worker.Models.ApiTypes;
using Newtonsoft.Json;

namespace EnLitenTelegramBot.Worker.Models
{
    public class ShrinkedResponseMesssage
    {
        [JsonPropertyName("chat_id")]
        public int ChatId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("parse_mode")]
        public string ParseMode { get; set; }
    }
}