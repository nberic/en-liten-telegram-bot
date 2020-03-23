
namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class Chat
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ChatPhoto Phot { get; set; }
        public string Description { get; set; }
        public string InviteLink { get; set; }
        public Message PinnedMessage { get; set; }
        public ChatPermissions Permissions { get; set; }
        public int? SlowModeDelay { get; set; }
        public string StickerSetName { get; set; }
        public bool? CanSetStickerSet { get; set; }

    }
}