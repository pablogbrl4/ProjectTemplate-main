using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Application.Services
{
    public class CicloAuditoriaApp : ICicloAuditoriaApp
    {
        private readonly ICicloAuditoriaService _cicloAuditoriaService;

        public CicloAuditoriaApp(ICicloAuditoriaService cicloAuditoriaService)
        {
            _cicloAuditoriaService = cicloAuditoriaService;
        }

        public CicloAuditoria GetCicloDadosApontamento(int idItem)
        {
            return _cicloAuditoriaService.GetCicloDadosApontamento(idItem);
        }
    }
}
