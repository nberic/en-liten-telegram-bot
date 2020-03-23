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
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
