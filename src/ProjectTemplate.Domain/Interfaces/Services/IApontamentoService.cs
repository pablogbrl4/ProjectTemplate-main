using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Domain.Interfaces.Services
{
    public interface IApontamentoService
    {
        void InserirDados(Mensagem[] mensagens);
    }
}
