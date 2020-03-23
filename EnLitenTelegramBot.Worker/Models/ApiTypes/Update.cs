
namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Update
    {
        public int UpdateId { get; set; }
        public Message Message { get; set; }
        public Message EditedMessage { get; set; }
        public Message ChannelPost { get; set; }
        public Message EditedChannelPost { get; set; }

        // TODO: Add fields for queries if needed
        public Poll Poll { get; set; }
        public PollAnswer PollAnswer { get; set; }
    }
}