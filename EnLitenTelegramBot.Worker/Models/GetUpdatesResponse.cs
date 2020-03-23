
using System.Collections.Generic;
using EnLitenTelegramBot.Worker.Models.ApiTypes;

namespace EnLitenTelegramBot.Worker.Models
{
    public class GetUpdatesResponse
    {
        public bool Ok { get; set; }
        public IEnumerable<Update> Result { get; set; }
    }
}