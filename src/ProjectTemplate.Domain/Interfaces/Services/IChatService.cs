using Orizon.Rest.Chat.Domain.Entities;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Interfaces.Services
{
    public interface IChatService
    {
        int Insert(int idLogin);

        void Lido(int idChat, int idLogin);

        IEnumerable<ChatE> Listar(int? idChat, int idLogin);

        IEnumerable<ChatE> Listar(int? idChat, int idLogin, string origem);
    }
}
