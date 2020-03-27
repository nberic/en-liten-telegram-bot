
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class ReplyKeyboardMarkup : IReplyMarkup
    {
        [JsonPropertyName("keyboard")]
        public List<List<KeyboardButton>> Keyboard { get; set; }
        
        [JsonPropertyName("resize_keyboard")]
        public bool? ResizeKeyboard { get; set; }

        [JsonPropertyName("one_time_keyboard")]
        public bool? OneTimeKeyboard { get; set; }

        [JsonPropertyName("selective")]
        public bool? Selective { get; set; }
    }
}