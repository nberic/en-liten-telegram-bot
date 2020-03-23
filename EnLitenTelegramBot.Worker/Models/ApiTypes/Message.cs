
using System;
using System.Collections.Generic;

namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Message
    {
        public int MessageId { get; set; }
        public User From { get; set; }
        public int Date { get; set; }
        public DateTime SentDate => UnixTimestampToDateTime(Date);
        public Chat Chat { get; set; }
        public User ForwardFrom { get; set; }
        public Chat ForwardFromChat { get; set; }
        public int? ForwardFromMessageId { get; set; }
        public string ForwardSignature { get; set; }
        public string ForwardSenderName { get; set; }
        public int? ForwardDate { get; set; }
        public DateTime ForwardSentDate => UnixTimestampToDateTime(ForwardDate);
        public Message ReplyToMessage { get; set; }
        public int? EditDate { get; set; }
        public DateTime LastEditDate => UnixTimestampToDateTime(EditDate);
        public string MediaGroupId { get; set; }
        public string AuthorSignature { get; set; }
        public string Text { get; set; }
        public IEnumerable<MessageEntity> Entities { get; set; }
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