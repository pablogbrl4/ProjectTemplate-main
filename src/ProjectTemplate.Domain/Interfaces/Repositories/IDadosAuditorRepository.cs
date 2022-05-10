using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Domain.Interfaces.Repositories
{
    public interface IDadosAuditorRepository
    {
        DadosAuditor GetDadosAuditorByIdLogin(int idlogin);
    }
}
