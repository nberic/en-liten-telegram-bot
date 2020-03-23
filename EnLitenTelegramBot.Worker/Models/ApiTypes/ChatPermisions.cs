
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class ChatPermissions
    {
        [JsonPropertyName("can_send_messages")]
        public bool? CanSendMessages { get; set; }

        [JsonPropertyName("can_send_media_messages")]
        public bool? CanSendMediaMessages { get; set; }

        [JsonPropertyName("can_send_polls")]
        public bool? CanSendPolls { get; set; }

        [JsonPropertyName("can_send_other_messages")]
        public bool? CanSendOtherMessages { get; set; }

        [JsonPropertyName("can_add_web_page_previews")]
        public bool? CanAddWebPagePreviews { get; set; }

        [JsonPropertyName("can_change_info")]
        public bool? CanChangeInfo { get; set; }

        [JsonPropertyName("can_invite_users")]
        public bool? CanInviteUsers { get; set; }

        [JsonPropertyName("can_pin_messages")]
        public bool? CanPinMessages { get; set; }
    }
}