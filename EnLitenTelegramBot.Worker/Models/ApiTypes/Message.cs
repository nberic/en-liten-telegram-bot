
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Message
    {
        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }

        [JsonPropertyName("from")]
        public User From { get; set; }

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonIgnore]
        public DateTime SentDate => UnixTimestampToDateTime(Date);

        [JsonPropertyName("chat")]
        public Chat Chat { get; set; }

        [JsonPropertyName("forward_from")]
        public User ForwardFrom { get; set; }

        [JsonPropertyName("forward_from_chat")]
        public Chat ForwardFromChat { get; set; }

        [JsonPropertyName("forward_from_message_id")]
        public int? ForwardFromMessageId { get; set; }

        [JsonPropertyName("forward_signature")]
        public string ForwardSignature { get; set; }

        [JsonPropertyName("forward_sender_name")]
        public string ForwardSenderName { get; set; }

        [JsonPropertyName("forward_date")]
        public int? ForwardDate { get; set; }

        [JsonIgnore]
        public DateTime ForwardSentDate => UnixTimestampToDateTime(ForwardDate);

        [JsonPropertyName("reply_to_message")]
        public Message ReplyToMessage { get; set; }

        [JsonPropertyName("edit_date")]
        public int? EditDate { get; set; }

        [JsonIgnore]
        public DateTime LastEditDate => UnixTimestampToDateTime(EditDate);

        [JsonPropertyName("media_group_id")]
        public string MediaGroupId { get; set; }

        [JsonPropertyName("author_signature")]
        public string AuthorSignature { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("entities")]
        public IEnumerable<MessageEntity> Entities { get; set; }

        [JsonPropertyName("caption_entities")]
        public IEnumerable<MessageEntity> CaptionEntities { get; set; }

        //TODO: add other possibly needed fields

        /// <summary>
        /// Converts UNIX timestamp to <c>System.DateTime</c>
        /// </summary>
        /// <param name="timestamp">Number of seconds since the epoch</param>
        /// <returns><c>System.DateTime</c> representation of UNIX timestamp</returns>
        private DateTime UnixTimestampToDateTime(int? timestamp)
        {
            var secondsCount = timestamp ?? throw new ArgumentException("The UNIX timestamp cannot be null.");
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = epoch.AddSeconds(secondsCount).ToLocalTime();
            return dateTime;
        }
    }
}