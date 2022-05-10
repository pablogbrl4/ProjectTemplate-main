using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class CicloAuditoriaService : ICicloAuditoriaService
    {
        private readonly ICicloAuditoriaRepository _cicloAuditoriaRepository;

        public CicloAuditoriaService(ICicloAuditoriaRepository cicloAuditoriaRepository)
        {
            _cicloAuditoriaRepository = cicloAuditoriaRepository;
        }

        public CicloAuditoria GetCicloDadosApontamento(int idItem)
        {
            return _cicloAuditoriaRepository.GetCicloDadosApontamento(idItem);
        }
    }
}
