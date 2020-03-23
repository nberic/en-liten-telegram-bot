
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Chat
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("photo")]
        public ChatPhoto Photo { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("invite_link")]
        public string InviteLink { get; set; }

        [JsonPropertyName("pinned_message")]
        public Message PinnedMessage { get; set; }

        [JsonPropertyName("permissions")]
        public ChatPermissions Permissions { get; set; }

        [JsonPropertyName("slow_mode_delay")]
        public int? SlowModeDelay { get; set; }

        [JsonPropertyName("sticker_set_name")]
        public string StickerSetName { get; set; }

        [JsonPropertyName("can_set_sticker_set")]
        public bool? CanSetStickerSet { get; set; }

    }
}