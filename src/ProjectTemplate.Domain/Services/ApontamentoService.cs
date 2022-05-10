using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class ApontamentoService : IApontamentoService
    {
        private readonly IApontamentoRepository _apontamentoRepository;

        public ApontamentoService(IApontamentoRepository apontamentoRepository)
        {
            _apontamentoRepository = apontamentoRepository;
        }

        public void InserirDados(Mensagem[] mensagens)
        {
            _apontamentoRepository.InserirDados(mensagens);
        }
    }
}
