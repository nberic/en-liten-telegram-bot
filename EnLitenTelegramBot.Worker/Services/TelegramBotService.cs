
using EnLitenTelegramBot.Worker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnLitenTelegramBot.Worker.Services
{
    public class TelegramBotService : IBotService
    {
        private readonly IBot _bot;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string GET_UPDATES_PATH = "getUpdates";

        public TelegramBotService(ILogger<TelegramBotService> logger, IBot bot, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _bot = bot;
            _httpClientFactory = httpClientFactory;
        }

        // TODO: Finish the code so it can parse the response
        // response model can be found in file 
        public async Task<string> GetUpdates()
        {
            _logger.LogInformation($"Getting updates from the URL: { _bot.UpdatesUrl }");
            var httpClient = _httpClientFactory.CreateClient();

            string response = null;
            try
            {
                response = await httpClient.GetStringAsync($"{ _bot.UpdatesUrl }");
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: { e.ToString() }");
            }

            _logger.LogInformation($"Returned payload is: { response }");
            return response;
        }
    }
}
 