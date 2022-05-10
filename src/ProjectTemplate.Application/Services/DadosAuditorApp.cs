using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Application.Services
{
    public class DadosAuditorApp : IDadosAuditorApp
    {
        private readonly IDadosAuditorService _dadosAuditorService;

        public DadosAuditorApp(IDadosAuditorService dadosAuditorService)
        {
            _dadosAuditorService = dadosAuditorService;
        }

        public DadosAuditor GetDadosAuditorByIdLogin(int idlogin)
        {
            return _dadosAuditorService.GetDadosAuditorByIdLogin(idlogin);
        }
    }
}
