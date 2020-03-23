
using System.Collections.Generic;
using System.Threading.Tasks;
using EnLitenTelegramBot.Worker.Models.ApiTypes;

namespace EnLitenTelegramBot.Worker.Services
{
    public interface IBotService
    {
        /// <summary>
        /// Get all message updates from the getUpdates URL
        /// </summary>
        /// <returns>List of Update objects</returns>
        Task<IEnumerable<Update>> GetUpdates();
        
        /// <summary>
        /// Send a message to a chat
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text which will be sent to the chat</param>
        /// <returns></returns>
        Task SendMessage(int chatId, string text);
    }
}