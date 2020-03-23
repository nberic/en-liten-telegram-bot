
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Update
    {
        [JsonPropertyName("update_id")]
        public int UpdateId { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("edited_message")]
        public Message EditedMessage { get; set; }

        [JsonPropertyName("channel_post")]
        public Message ChannelPost { get; set; }

        [JsonPropertyName("edited_channel_post")]
        public Message EditedChannelPost { get; set; }

        // TODO: Add fields for queries if needed

        [JsonPropertyName("poll")]
        public Poll Poll { get; set; }

        [JsonPropertyName("poll_answer")]
        public PollAnswer PollAnswer { get; set; }
    }
}