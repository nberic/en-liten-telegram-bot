
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class PollOption
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("voter_count")]
        public int VoterCount { get; set; }
    }
}