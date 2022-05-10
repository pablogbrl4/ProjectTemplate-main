using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Application.Services
{
    public class ApontamentoApp : IApontamentoApp
    {
        private readonly IApontamentoService _apontamentoService;

        public ApontamentoApp(IApontamentoService apontamentoService)
        {
            _apontamentoService = apontamentoService;
        }

        public void InserirDados(Mensagem[] mensagens)
        {
            _apontamentoService.InserirDados(mensagens);
        }
    }
}
