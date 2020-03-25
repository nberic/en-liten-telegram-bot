using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class KeyboardButton
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        // TODO: Set these fields as ignored when null
        // [JsonPropertyName("request_contact")]
        // public bool? RequestContact { get; set; }

        // [JsonPropertyName("request_location")]
        // public bool? RequestLocation { get; set; }

        // [JsonPropertyName("request_poll")]
        // public KeyboardButtonPollType RequestPoll { get; set; }
    }
}