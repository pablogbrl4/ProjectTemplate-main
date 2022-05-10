using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Application.Services
{
    public class ChatConversasApp : IChatConversasApp
    {
        private readonly IChatConversasService _chatConversasService;

        public ChatConversasApp(IChatConversasService chatConversasService)
        {
            _chatConversasService = chatConversasService;
        }

        public void AtualizarMensagemChat(int fkchat, int IdChatConversas, string conversa)
        {
            _chatConversasService.AtualizarMensagemChat(fkchat, IdChatConversas, conversa);
        }

        public bool BuscarChatRemetente(int IdChatConversas, int remetente, int fkChat)
        {
            return _chatConversasService.BuscarChatRemetente(IdChatConversas, remetente, fkChat);
        }

        public int BuscarUltimaMensagemChat(int FkChat, int remetente)
        {
            return _chatConversasService.BuscarUltimaMensagemChat(FkChat, remetente);
        }

        public void DeletarChatConversas(int fkchat, int IdChatConversas, int idRemetente)
        {
             _chatConversasService.DeletarChatConversas(fkchat, IdChatConversas, idRemetente);
        }

        public void Insert(Mensagem mensagem, string origem)
        {
            _chatConversasService.Insert(mensagem, origem);
        }

        public IEnumerable<ChatConversas> Listar(int fkChat)
        {
            return _chatConversasService.Listar(fkChat);
        }

        public IEnumerable<ChatConversas> Listar(int fkChat, string origem)
        {
            return _chatConversasService.Listar(fkChat, origem);
        }

        public IEnumerable<ChatConversas> ListarPorFkChatConversa(int fkChat, string conversa)
        {
            return _chatConversasService.ListarPorFkChatConversa(fkChat, conversa);
        }
    }
}
