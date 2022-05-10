using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class DadosAuditorService : IDadosAuditorService
    {
        private readonly IDadosAuditorRepository _dadosAuditorRepository;

        public DadosAuditorService(IDadosAuditorRepository dadosAuditorRepository)
        {
            _dadosAuditorRepository = dadosAuditorRepository;
        }

        public DadosAuditor GetDadosAuditorByIdLogin(int idlogin)
        {
            return _dadosAuditorRepository.GetDadosAuditorByIdLogin(idlogin);
        }
    }
}
