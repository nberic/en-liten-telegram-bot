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

        public Worker(ILogger<Worker> logger, IBotService botService)
        {
            _logger = logger;
            _botService = botService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at { DateTimeOffset.Now }");
            
            IEnumerable<Update> updates = null;
            while (!stoppingToken.IsCancellationRequested)
            {

                #region GetUpdates
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
                
                _logger.LogInformation($"Returned updates: { JsonSerializer.Serialize(updates) }");
                #endregion

                #region RespondToUpdates
                foreach (var update in updates)
                {
                    _logger.LogInformation($"Processing update with ID: { update.UpdateId } which contains message by ID: { update.Message.MessageId } and text: { update.Message.Text }");
                    try
                    {
                        await _botService.SendTextMessageAsync(update.Message.Chat.Id, 
                            "How <i><b>you</b></i> doin'?", 
                            update.UpdateId);
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

                // set values for the reply keyboard
                var replyKeyboard = new ReplyKeyboardMarkup();
                replyKeyboard.Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>()
                    {
                        new KeyboardButton() { Text = "1" },
                        new KeyboardButton() { Text = "2" },
                        new KeyboardButton() { Text = "3" }
                    },
                    new List<KeyboardButton>()
                    {
                        new KeyboardButton() { Text = "4" },
                        new KeyboardButton() { Text = "5" },
                        new KeyboardButton() { Text = "6" }
                    },
                    new List<KeyboardButton>()
                    {
                        new KeyboardButton() { Text = "7" },
                        new KeyboardButton() { Text = "8" },
                        new KeyboardButton() { Text = "9" }
                    },
                };
                await _botService.SendKeyboardMessageAsync(1127648888, "What is your favorite number?", replyKeyboard, 907889274);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
