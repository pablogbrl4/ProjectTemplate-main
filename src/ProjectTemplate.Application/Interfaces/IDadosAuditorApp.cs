using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Application.Interfaces
{
    public interface IDadosAuditorApp
    {
        DadosAuditor GetDadosAuditorByIdLogin(int idlogin);
    }
}
