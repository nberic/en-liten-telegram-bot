using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EnLitenTelegramBot.Worker.Models;
using EnLitenTelegramBot.Worker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnLitenTelegramBot.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    var botConfiguration = hostContext.Configuration
                        .GetSection("BotConfiguration")
                        .Get<BotConfiguration>();
                    services.AddSingleton(botConfiguration);
                    services.AddSingleton<IBot, TelegramBot>();
                    services.AddSingleton<IBotService, TelegramBotService>();
                    services.AddHttpClient();
                });
    }
}
