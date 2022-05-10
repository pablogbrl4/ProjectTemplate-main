using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Domain.Interfaces.Repositories
{
    public interface ICicloAuditoriaRepository
    {
        CicloAuditoria GetCicloDadosApontamento(int idItem);
    }
}
