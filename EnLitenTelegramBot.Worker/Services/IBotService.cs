
using System.Collections.Generic;
using System.Threading.Tasks;
using EnLitenTelegramBot.Worker.Models.ApiTypes;

namespace EnLitenTelegramBot.Worker.Services
{
    public interface IBotService
    {
        Task<IEnumerable<Update>> GetUpdates();
    }
}