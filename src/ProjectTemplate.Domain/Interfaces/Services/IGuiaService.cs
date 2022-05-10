using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Domain.Interfaces.Services
{
    public interface IGuiaService
    {
        void AtribuirNomeAuditorUltimaModificacao(Guia model);
        void AtribuirNomePrestadorUltimaModificacao(Guia model);
    }
}
