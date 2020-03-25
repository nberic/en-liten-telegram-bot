
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class KeyboardButtonPollType
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}