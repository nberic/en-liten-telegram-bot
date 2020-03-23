
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class ChatPhoto
    {
        [JsonPropertyName("small_file_id")]
        public string SmallFileId { get; set; }

        [JsonPropertyName("small_file_unique_id")]
        public string SmallFileUniqueId { get; set; }

        [JsonPropertyName("big_file_id")]
        public string BigFileId { get; set; }

        [JsonPropertyName("big_file_unique_id")]
        public string BigFileUniqueId { get; set; }
    }
}