using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Domain.Interfaces.Services
{
    public interface IDadosAuditorService
    {
        DadosAuditor GetDadosAuditorByIdLogin(int idlogin);

    }
}
