using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Application.Interfaces
{
    public interface ICicloAuditoriaApp
    {
        CicloAuditoria GetCicloDadosApontamento(int idItem);
    }
}
