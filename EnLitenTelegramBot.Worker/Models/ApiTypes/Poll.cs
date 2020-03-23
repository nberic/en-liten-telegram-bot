
using System.Collections.Generic;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<PollOption> Options { get; set; }
        public int TotalVoterCount { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAnonymous { get; set; }
        public string Type { get; set; }
        public bool AllowsMultipleAnswers { get; set; }
        public int CorrectOptionId { get; set; }
    }
}