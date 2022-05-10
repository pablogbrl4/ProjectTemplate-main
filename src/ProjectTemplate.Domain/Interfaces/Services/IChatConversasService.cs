using Orizon.Rest.Chat.Domain.Entities;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Interfaces.Services
{
    public interface IChatConversasService
    {
        void Insert(Mensagem mensagem, string origem);
        void AtualizarMensagemChat(int fkchat, int IdChatConversas, string conversa);
        int BuscarUltimaMensagemChat(int FkChat, int remetente);
        bool BuscarChatRemetente(int IdChatConversas, int remetente, int fkChat);
        void DeletarChatConversas(int fkchat, int IdChatConversas, int idRemetente);
        IEnumerable<ChatConversas> Listar(int fkChat);
        IEnumerable<ChatConversas> Listar(int fkChat, string origem);
        IEnumerable<ChatConversas> ListarPorFkChatConversa(int fkChat, string conversa);
    }
}
