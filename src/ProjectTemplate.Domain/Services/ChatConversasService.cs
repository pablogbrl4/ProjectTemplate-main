using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class ChatConversasService : IChatConversasService
    {
        private readonly IChatConversasRepository _chatConversasRepository;

        public ChatConversasService(IChatConversasRepository chatConversasRepository)
        {
            _chatConversasRepository = chatConversasRepository;
        }

        public void AtualizarMensagemChat(int fkchat, int IdChatConversas, string conversa)
        {
            _chatConversasRepository.AtualizarMensagemChat(fkchat, IdChatConversas, conversa);
        }

        public bool BuscarChatRemetente(int IdChatConversas, int remetente, int fkChat)
        {
            return _chatConversasRepository.BuscarChatRemetente(IdChatConversas, remetente, fkChat);
        }

        public int BuscarUltimaMensagemChat(int FkChat, int remetente)
        {
            return _chatConversasRepository.BuscarUltimaMensagemChat(FkChat, remetente);
        }

        public void DeletarChatConversas(int fkchat, int IdChatConversas, int idRemetente)
        {
            _chatConversasRepository.DeletarChatConversas(fkchat, IdChatConversas, idRemetente);
        }

        public void Insert(Mensagem mensagem, string origem)
        {
            _chatConversasRepository.Insert(mensagem, origem);
        }

        public IEnumerable<ChatConversas> Listar(int fkChat)
        {
            return _chatConversasRepository.Listar(fkChat);
        }

        public IEnumerable<ChatConversas> Listar(int fkChat, string origem)
        {
            return _chatConversasRepository.Listar(fkChat, origem);
        }

        public IEnumerable<ChatConversas> ListarPorFkChatConversa(int fkChat, string conversa)
        {
            return _chatConversasRepository.ListarPorFkChatConversa(fkChat, conversa);
        }
    }
}
