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
using Serilog;

namespace EnLitenTelegramBot.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mainConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(mainConfiguration)
                .CreateLogger();
            
            try
            {
                Log.Information("Bot is starting up.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The bot was unable to start due to this error.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
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
