using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Domain.Interfaces.Services
{
    public interface ICicloAuditoriaService
    {
        CicloAuditoria GetCicloDadosApontamento(int idItem);
    }
}
