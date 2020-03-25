
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public interface IReplyMarkup
    {
        [JsonPropertyName("keyboard")]
        IEnumerable<IEnumerable<KeyboardButton>> Keyboard { get; set; }
    }
}