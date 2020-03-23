
using System;

namespace EnLitenTelegramBot.Worker.Models
{
    public interface IBot
    {
        int HighestRespondedMessageId { get; set; }
        string ApiUrl { get; }
        string UpdatesUrl { get; }
        string SendUrl { get; }
    }
}