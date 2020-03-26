
using System.Collections.Generic;

namespace EnLitenTelegramBot.Worker.Models
{
    public class BotConfiguration
    {
        public string Token { get; set; }
        public string UpdatesMethod { get; set; }
        public string SendMethod { get; set; }
        public int HighestRespondedUpdateId { get; set; }
        public IEnumerable<QuizQuestion> QuizQuestions { get; set; }
    }
}
