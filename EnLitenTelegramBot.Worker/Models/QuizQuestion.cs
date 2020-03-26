
using System.Collections.Generic;

namespace EnLitenTelegramBot.Worker.Models
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public IEnumerable<IEnumerable<Answer>> Answers { get; set; }
    }
}