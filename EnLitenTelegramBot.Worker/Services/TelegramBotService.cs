
using EnLitenTelegramBot.Worker.Models;
using EnLitenTelegramBot.Worker.Models.ApiTypes;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
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
        public async Task<IEnumerable<Update>> GetUpdatesAsync(Dictionary<string, LatestUserResponse> previousResponses)
        {
            var momentOfEntry = DateTime.Now;
            _logger.LogInformation("Getting updates from the URL: {updatesUrl}", _bot.UpdatesUrl);
            var httpClient = _httpClientFactory.CreateClient();

            IEnumerable<Update> updates = null;
            try
            {
                var payload = await httpClient.GetStreamAsync(_bot.UpdatesUrl);
                var getUpdatesResponse = await JsonSerializer.DeserializeAsync<GetUpdatesResponse>(payload);
                _logger.LogInformation("Returned payload is: {payload}", JsonSerializer.Serialize(getUpdatesResponse));

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

            _logger.LogInformation("Returned payload is: {payload}", JsonSerializer.Serialize(updates));

            return updates.Where(update => IsLatestUserUpdate(update, previousResponses, momentOfEntry));
        }

        /// <summary>
        /// Decides if the bot should respond to a message update,
        /// depending on date of the message sending,
        /// type of conversation etc.
        /// </summary>
        /// <param name="update"> Message update for which we are deciding if we are going to respond to it</param>
        /// <param name="previousResponses"> Dictionary of previous responses by users </param>
        /// <returns> Bool which means should we respond to the update or not </returns>
        private bool IsLatestUserUpdate(Update update, Dictionary<string, LatestUserResponse> previousResponses, DateTime momentOfEntry)
        {
            const int MESSAGE_DURATION_OFFSET_IN_SECONDS = -15;
            var tresholdMoment = momentOfEntry.AddSeconds(MESSAGE_DURATION_OFFSET_IN_SECONDS);
            var messageDate = Message.UnixTimestampToDateTime(update.Message.Date);
            if (messageDate < tresholdMoment || !update.Message.Chat.Type.Equals("private"))
            {
                return false;
            }
            LatestUserResponse userResponse = null;
            previousResponses.TryGetValue(update.Message.From.Id.ToString(), out userResponse);
            int previouslyAskedQuestionIndex = userResponse?.LatestUpdateId ?? 0;
            bool isQuizFinished = userResponse?.IsQuizFinished ?? false;
            // respond only to private chats
            return !isQuizFinished && update.UpdateId > previouslyAskedQuestionIndex;
        }

        /// <summary>
        /// Send a text message to a chat
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text which will be sent to the chat</param>
        /// <returns>Task</returns>
        public async Task SendTextMessageAsync(int chatId, string text)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var payloadContent = new ShrinkedResponseMesssage()
            {
                ChatId = chatId,
                Text = text,
                ParseMode = "HTML"
            };
            var serializedPayloadContent = JsonSerializer.Serialize(payloadContent);
            var payload = new StringContent(serializedPayloadContent,
                Encoding.UTF8,
                "application/json");
            _logger.LogInformation("Sending message by posting to the URL: {sendUrl}, to chat with ID: {chatId} and message content of: \"{messageText}\"", _bot.SendUrl, chatId, text);

            try
            {
                var response = await httpClient.PostAsync(_bot.SendUrl, payload);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Response returned successfully with status code: {statusCode}", response.StatusCode);
                }
                else
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Response returned error with status code: {statusCode} and contents: {contents}", response.StatusCode, contents);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
        }

        /// <summary>
        /// Send a message with a reply keyboard
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="previouslyAskedQuestionIndex"> Index of previous question to which the user has answered </param>
        /// <returns></returns>
        public async Task SendKeyboardMessageAsync(int chatId, int previouslyAskedQuestionIndex)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var text = _bot.QuizQuestions[previouslyAskedQuestionIndex + 1].Question;

            // set values for the reply keyboard
            var replyKeyboard = new ReplyKeyboardMarkup();
            replyKeyboard.Keyboard = new List<List<KeyboardButton>>();
            foreach (var row in _bot.QuizQuestions[previouslyAskedQuestionIndex + 1].Answers)
            {
                var rowOfAnswers = new List<KeyboardButton>();
                foreach (var answer in row)
                {
                    rowOfAnswers.Add(new KeyboardButton() { Text = answer.Text });
                }
                replyKeyboard.Keyboard.Add(rowOfAnswers);
            }

            var payloadContent = new ResponseMesssage()
            {
                ChatId = chatId,
                Text = text,
                ParseMode = "HTML",
                ReplyMarkup = replyKeyboard
            };
            var serializedPayloadContent = JsonSerializer.Serialize(payloadContent);
            var payload = new StringContent(serializedPayloadContent,
                Encoding.UTF8,
                "application/json");
            _logger.LogInformation("Sending message by posting to the URL: {sendUrl}, to chat with ID: {chatId} and message content of: {messageText}", _bot.SendUrl, chatId, text);

            try
            {
                var response = await httpClient.PostAsync(_bot.SendUrl, payload);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Response returned successfully with status code: {statusCode}", response.StatusCode);
                }
                else
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Response returned error with status code: {statusCode} and contents: {contents}", response.StatusCode, contents);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending the message");
            }
        }
    }
}
