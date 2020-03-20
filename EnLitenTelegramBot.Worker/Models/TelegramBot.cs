
namespace EnLitenTelegramBot.Worker.Models
{
    public class TelegramBot : IBot
    {
        private readonly BotConfiguration _botConfiguration;

        public TelegramBot(BotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
        }
        public string ApiUrl => $"https://api.telegram.org/bot{ _botConfiguration.Token }";
    }
}