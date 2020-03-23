
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Poll
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("options")]
        public IEnumerable<PollOption> Options { get; set; }

        [JsonPropertyName("total_voter_count")]
        public int TotalVoterCount { get; set; }

        [JsonPropertyName("is_closed")]
        public bool IsClosed { get; set; }

        [JsonPropertyName("is_anonymous")]
        public bool IsAnonymous { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("allows_multiple_answers")]
        public bool AllowsMultipleAnswers { get; set; }

        [JsonPropertyName("correct_option_id")]
        public int CorrectOptionId { get; set; }
    }
}