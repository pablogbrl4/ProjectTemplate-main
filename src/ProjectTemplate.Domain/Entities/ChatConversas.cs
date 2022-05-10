using System;

namespace Orizon.Rest.Chat.Domain.Entities
{
    public class ChatConversas
    {
        public int IdChatConversas { get; set; }
        public int FkChat { get; set; }
        public string Conversa { get; set; }
        public DateTime? Data { get; set; }
        public int IdLoginRemetente { get; set; }
        public string DsLoginRemetente { get; set; }
        public string Origem { get; set; }
        public bool FlgManual { get; set; }
    }
}
