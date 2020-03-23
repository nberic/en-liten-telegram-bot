using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                    updates = await _botService.GetUpdates();
                }
                catch (HttpRequestException e)
                {
                    _logger.LogCritical(e.StackTrace);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.StackTrace);
                }
                
                _logger.LogInformation($"Returned updates: { updates }");
                #endregion

                #region RespondToUpdates
                foreach (var update in updates)
                {
                    _logger.LogInformation($"Processing update with ID: { update.UpdateId } which contains message by ID: { update.Message.MessageId } and text: { update.Message.Text }");
                    try
                    {
                        await _botService.SendMessage(update.Message.Chat.Id, 
                            "How <i><b>you</b></i> doin'?", 
                            update.Message.MessageId);
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
