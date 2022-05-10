using System;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Entities
{
    public class ChatE
    {
        public int IdChat { get; set; }
        public DateTime? Data { get; set; }
        public int IdLogin { get; set; }
        public int QtdeNaoLidas { get; set; }
        public IEnumerable<ChatConversas> Conversas { get; set; }
    }
}
