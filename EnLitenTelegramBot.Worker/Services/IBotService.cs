
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
        Task<IEnumerable<Update>> GetUpdatesAsync();
        
        /// <summary>
        /// Send a text message to a chat
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text which will be sent to the chat</param>
        /// <param name="updateId"> ID of the update the message corresponds to </param>
        /// <returns>Task</returns>
        Task SendTextMessageAsync(int chatId, string text, int updateId);

        /// <summary>
        /// Send a message with a reply keyboard
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text part of the message that will be sent </param>
        /// <param name="updateId"> ID of the update the message corresponds to</param>
        /// <returns>Task</returns>
        Task SendKeyboardMessageAsync(int chatId, string text, IReplyMarkup replyMarkup, int updateId);
    }
}