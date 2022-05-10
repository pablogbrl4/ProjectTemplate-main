using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Application.Interfaces
{
    public interface IApontamentoApp
    {
        void InserirDados(Mensagem[] mensagens);
    }
}
