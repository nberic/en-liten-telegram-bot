
using System;

namespace EnLitenTelegramBot.Worker.Models
{
    public interface IBot
    {
        public string ApiUrl { get; }
        public string UpdatesUrl { get; }
    }
}