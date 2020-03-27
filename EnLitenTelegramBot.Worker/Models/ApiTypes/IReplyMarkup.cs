
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public interface IReplyMarkup
    {
        [JsonPropertyName("keyboard")]
        List<List<KeyboardButton>> Keyboard { get; set; }
    }
}