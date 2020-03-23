using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models
{
    public class ResponseMesssage
    {
        [JsonPropertyName("chat_id")]
        public int ChatId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("parse_mode")]
        public string ParseMode { get; set; }
    }
}