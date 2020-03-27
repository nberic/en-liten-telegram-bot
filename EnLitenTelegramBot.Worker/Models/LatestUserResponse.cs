
namespace EnLitenTelegramBot.Worker.Models
{
    public class LatestUserResponse
    {
        // public int UserId { get; set; }
        public int PreviouslyAskedQuestionIndex { get; set; }
        public int LatestUpdateId { get; set; }
        public bool IsQuizFinished { get; set; } = false;
    }
}