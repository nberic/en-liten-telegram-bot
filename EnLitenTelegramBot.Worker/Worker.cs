using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EnLitenTelegramBot.Worker.Models;
using EnLitenTelegramBot.Worker.Models.ApiTypes;
using EnLitenTelegramBot.Worker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EnLitenTelegramBot.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBotService _botService;
        private Dictionary<string, int> previousResponses;
        private const string START_MESSAGE = "/start";
        private const int FIRST_QUESTION_INDEX = 0;
        private readonly string DEFAULT_UPDATE_RESONSE;

        public Worker(ILogger<Worker> logger, IBotService botService)
        {
            _logger = logger;
            _botService = botService;
            DEFAULT_UPDATE_RESONSE = $"Sorry, but I can't understand you.\r\nIn order to start a quiz type {START_MESSAGE}";
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            previousResponses = new Dictionary<string, int>();
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at {StartingTime}", DateTimeOffset.Now);
            
            IEnumerable<Update> updates = null;
            int previouslyAskedQuestionIndex = 0; // the index of question that was previously asked

            while (!stoppingToken.IsCancellationRequested)
            {

                #region GetUpdates
                // get message updates
                try
                {
                    updates = await _botService.GetUpdatesAsync();
                }
                catch (HttpRequestException e)
                {
                    _logger.LogCritical(e.StackTrace);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.StackTrace);
                }
                
                _logger.LogInformation("Returned updates: {Updates}", JsonSerializer.Serialize(updates));
                #endregion

                #region RespondToUpdates
                // respond to each message update
                foreach (var update in updates)
                {
                    _logger.LogInformation("Processing update with ID: {updateId} which contains message by ID: {messageId} and text: {messageText}", update.UpdateId, update.Message.MessageId, update.Message.Text);
                    
                    // dont' answer to anyone but me
                    if (!update.Message.From.Username.Equals("nberic"))
                    {
                        continue;
                    }

                    // respond to message update depending on message content and previous question index
                    try
                    {
                        // if it's the /start message
                        if (update.Message.Text.Equals(START_MESSAGE))
                        {
                            await _botService.SendKeyboardMessageAsync(update.Message.Chat.Id, FIRST_QUESTION_INDEX);

                            _logger.LogInformation("Question number {questionNumber} sent to user {userId}", FIRST_QUESTION_INDEX, update.Message.From.Id);
                            previousResponses.Add(update.Message.From.Id.ToString(), FIRST_QUESTION_INDEX);
                        }
                        // if it's a response to an answer
                        else if (previousResponses.TryGetValue(update.Message.From.Id.ToString(), out previouslyAskedQuestionIndex))
                        {
                            await _botService.SendKeyboardMessageAsync(update.Message.Chat.Id, previouslyAskedQuestionIndex);

                            previousResponses[update.Message.From.Id.ToString()] = ++previouslyAskedQuestionIndex;
                            _logger.LogInformation("Question number {questionNumber} sent to user {userId}", previouslyAskedQuestionIndex, update.Message.From.Id);
                        }
                        // if it's any other message
                        else
                        {
                            await _botService.SendTextMessageAsync(update.Message.Chat.Id, DEFAULT_UPDATE_RESONSE);
                                
                            _logger.LogInformation("Generic response sent to user {userId} for unprocessable message with text {messageText}", update.Message.From.Id, update.Message.Text);
                            
                        }
                    }
                    catch(HttpRequestException e)
                    {
                        _logger.LogCritical(e.StackTrace);
                    }
                    catch(Exception e)
                    {
                        _logger.LogCritical(e.StackTrace);
                    }
                }
                #endregion

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
