
using System.Collections.Generic;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class PollAnswer
    {
        public int PollId { get; set; }
        public User User { get; set; }
        public IEnumerable<int> OptionIds { get; set; }
    }
}