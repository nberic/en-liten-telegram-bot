
using System;

namespace EnLitenTelegramBot.Worker.Models
{
    public interface IBot
    {
        string ApiUrl { get; }
        string UpdatesUrl { get; }
        string SendUrl { get; }
    }
}