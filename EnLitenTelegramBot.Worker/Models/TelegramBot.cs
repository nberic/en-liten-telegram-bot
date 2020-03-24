
namespace EnLitenTelegramBot.Worker.Models
{
    public class TelegramBot : IBot
    {
        private readonly BotConfiguration _botConfiguration;

        public TelegramBot(BotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
            HighestRespondedUpdateId = _botConfiguration.HighestRespondedUpdateId;
        }
        public int HighestRespondedUpdateId { get; set; }
        public string ApiUrl => $"https://api.telegram.org/bot{ _botConfiguration.Token }";
        public string UpdatesUrl => $"{ ApiUrl }/{ _botConfiguration.UpdatesMethod }";
        public string SendUrl => $"{ ApiUrl }/{ _botConfiguration.SendMethod }";
    }
}