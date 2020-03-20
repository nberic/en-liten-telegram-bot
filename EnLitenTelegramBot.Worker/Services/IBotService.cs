
using System.Threading.Tasks;

namespace EnLitenTelegramBot.Worker.Services
{
    public interface IBotService
    {
        Task<string> GetUpdates();
    }
}