
using EnLitenTelegramBot.Worker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using EnLitenTelegramBot.Worker.Models.ApiTypes;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EnLitenTelegramBot.Worker.Services
{
    public class TelegramBotService : IBotService
    {
        private readonly IBot _bot;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TelegramBotService(ILogger<TelegramBotService> logger, IBot bot, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _bot = bot;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get all message updates from the getUpdates URL
        /// </summary>
        /// <returns>List of Update objects</returns>
        public async Task<IEnumerable<Update>> GetUpdatesAsync()
        {
            _logger.LogInformation("Getting updates from the URL: { updatesUrl }", _bot.UpdatesUrl);
            var httpClient = _httpClientFactory.CreateClient();

            IEnumerable<Update> updates = null;
            try
            {
                var payload = await httpClient.GetStreamAsync(_bot.UpdatesUrl);
                var getUpdatesResponse = await JsonSerializer.DeserializeAsync<GetUpdatesResponse>(payload);
                _logger.LogInformation("Returned payload is: { payload }", JsonSerializer.Serialize(getUpdatesResponse));
                
                updates = getUpdatesResponse.Result;

            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            _logger.LogInformation("Returned payload is: { payload }", JsonSerializer.Serialize(updates));

            // return only updates to which it hasn't been responded
            return updates.Where(update => update.UpdateId > _bot.HighestRespondedUpdateId);
        }

        /// <summary>
        /// Send a text message to a chat
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text which will be sent to the chat</param>
        /// <param name="updateId"> ID of the update the message corresponds to </param>
        /// <returns>Task</returns>
        public async Task SendTextMessageAsync(int chatId, string text, int updateId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var payloadContent = new ResponseMesssage()
            {
                ChatId = chatId,
                Text = text,
                ParseMode = "HTML"
            };
            var payload = new StringContent(JsonSerializer.Serialize(payloadContent), 
                Encoding.UTF8, 
                "application/json");
            _logger.LogInformation("Sending message by posting to the URL: { sendUrl }, to chat with ID: { chatId } and message content of: { messageText }", _bot.SendUrl, chatId, text);

            try
            {
                var response = await httpClient.PostAsync(_bot.SendUrl, payload);
                _logger.LogInformation("Response returned with status code: { statusCode } and the reason phrase: { responsePhrase }", response.StatusCode, response.ReasonPhrase);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
            // update the highest responed messageId
            _bot.HighestRespondedUpdateId = updateId > _bot.HighestRespondedUpdateId
                ? updateId
                : _bot.HighestRespondedUpdateId;
        }

        /// <summary>
        /// Send a message with a reply keyboard
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text part of the message that will be sent </param>
        /// <param name="updateId"> ID of the update the message corresponds to</param>
        /// <returns></returns>
        public async Task SendKeyboardMessageAsync(int chatId, string text, IReplyMarkup replyMarkup, int updateId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var payloadContent = new ResponseMesssage()
            {
                ChatId = chatId,
                Text = text,
                ParseMode = "HTML",
                ReplyMarkup = replyMarkup
            };
            var serializedPayloadContent = JsonSerializer.Serialize(payloadContent);
            var payload = new StringContent(serializedPayloadContent, 
                Encoding.UTF8, 
                "application/json");
            _logger.LogInformation("Sending message by posting to the URL: { sendUrl }, to chat with ID: { chatId } and message content of: { messageText }", _bot.SendUrl, chatId, text);

            try
            {
                var response = await httpClient.PostAsync(_bot.SendUrl, payload);
                _logger.LogInformation("Response returned with status code: { statusCode } and the reason phrase: { reasonPhrase }", response.StatusCode, response.ReasonPhrase);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
            // update the highest responed messageId
            _bot.HighestRespondedUpdateId = updateId > _bot.HighestRespondedUpdateId
                ? updateId
                : _bot.HighestRespondedUpdateId;
        }
    }
}
 