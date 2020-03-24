
using System;

namespace EnLitenTelegramBot.Worker.Models
{
    public interface IBot
    {
        int HighestRespondedUpdateId { get; set; }
        string ApiUrl { get; }
        string UpdatesUrl { get; }
        string SendUrl { get; }
    }
}