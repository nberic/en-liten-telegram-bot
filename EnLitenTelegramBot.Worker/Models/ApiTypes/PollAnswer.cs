
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class PollAnswer
    {
        [JsonPropertyName("poll_id")]
        public int PollId { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("option_ids")]
        public IEnumerable<int> OptionIds { get; set; }
    }
}