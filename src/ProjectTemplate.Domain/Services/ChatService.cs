using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public int Insert(int idLogin)
        {
            return _chatRepository.Insert(idLogin);
        }

        public void Lido(int idChat, int idLogin)
        {
            _chatRepository.Lido(idChat, idLogin);
        }

        public IEnumerable<ChatE> Listar(int? idChat, int idLogin)
        {
            return _chatRepository.Listar(idChat, idLogin, null);
        }

        public IEnumerable<ChatE> Listar(int? idChat, int idLogin, string origem)
        {
            return _chatRepository.Listar(idChat, idLogin, origem);
        }
    }
}
