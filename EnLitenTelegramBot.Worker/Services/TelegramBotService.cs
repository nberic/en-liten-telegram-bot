
using EnLitenTelegramBot.Worker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using EnLitenTelegramBot.Worker.Models.ApiTypes;
using System.Collections.Generic;

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
        public async Task<IEnumerable<Update>> GetUpdates()
        {
            _logger.LogInformation($"Getting updates from the URL: { _bot.UpdatesUrl }");
            var httpClient = _httpClientFactory.CreateClient();

            IEnumerable<Update> updates = null;
            try
            {
                var payload = await httpClient.GetStreamAsync(_bot.UpdatesUrl);
                var getUpdatesResponse = await JsonSerializer.DeserializeAsync<GetUpdatesResponse>(payload);
                _logger.LogInformation($"Returned payload is: { JsonSerializer.Serialize(getUpdatesResponse) }");
                
                updates = getUpdatesResponse.Result;

            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Exception occured: { e.ToString() }");
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occured: { e.ToString() }");
            }

            _logger.LogInformation($"Returned payload is: { updates }");
            return updates;
        }
    }
}
 