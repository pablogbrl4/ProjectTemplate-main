using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Application.Services
{
    public class ChatApp : IChatApp
    {
        private readonly IChatService _chatService;

        public ChatApp(IChatService chatService)
        {
            _chatService = chatService;
        }

        public int Insert(int idLogin)
        {
            return _chatService.Insert(idLogin);
        }

        public void Lido(int idChat, int idLogin)
        {
            _chatService.Lido(idChat, idLogin);
        }

        public IEnumerable<ChatE> Listar(int? idChat, int idLogin)
        {
            return _chatService.Listar(idChat, idLogin, null);
        }

        public IEnumerable<ChatE> Listar(int? idChat, int idLogin, string origem)
        {
            return _chatService.Listar(idChat, idLogin, origem);
        }
    }
}
