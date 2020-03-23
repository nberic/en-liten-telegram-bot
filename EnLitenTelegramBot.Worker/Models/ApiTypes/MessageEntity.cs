namespace EnLitenTelegramBot.Worker.Models.ApiTypes
{
    public class MessageEntity
    {
        public string Type { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
        public string Language { get; set; }
    }
}