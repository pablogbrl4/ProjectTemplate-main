using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Application.Interfaces
{
    public interface IGuiaApp
    {
        void AtribuirNomeAuditorUltimaModificacao(Guia model);
        void AtribuirNomePrestadorUltimaModificacao(Guia model);
    }
}
