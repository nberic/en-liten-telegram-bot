
using System.Collections.Generic;
using System.Threading.Tasks;
using EnLitenTelegramBot.Worker.Models;
using EnLitenTelegramBot.Worker.Models.ApiTypes;

namespace EnLitenTelegramBot.Worker.Services
{
    public interface IBotService
    {
        /// <summary>
        /// Get all message updates from the getUpdates URL
        /// </summary>
        /// <param name="previousResponses"> Dictionary of latest responses by users</param>
        /// <returns>List of Update objects</returns>
        Task<IEnumerable<Update>> GetUpdatesAsync(Dictionary<string, LatestUserResponse> previousResponses);
        
        /// <summary>
        /// Send a text message to a chat
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="text"> Text which will be sent to the chat</param>
        /// <returns>Task</returns>
        Task SendTextMessageAsync(int chatId, string text);

        /// <summary>
        /// Send a message with a reply keyboard
        /// </summary>
        /// <param name="chatId"> ID of the chat to which the message will be sent</param>
        /// <param name="previouslyAskedQuestionIndex"> Index of previous question to which the user has answered </param>
        /// <returns>Task</returns>
        Task SendKeyboardMessageAsync(int chatId, int previouslyAskedQuestionIndex);
    }
}