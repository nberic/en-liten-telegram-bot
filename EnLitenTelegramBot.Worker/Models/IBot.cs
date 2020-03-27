
using System;
using System.Collections.Generic;

namespace EnLitenTelegramBot.Worker.Models
{
    public interface IBot
    {
        string ApiUrl { get; }
        string UpdatesUrl { get; }
        string SendUrl { get; }
        List<QuizQuestion> QuizQuestions { get; }
    }
}