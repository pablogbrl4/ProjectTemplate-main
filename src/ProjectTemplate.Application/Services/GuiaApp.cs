using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Application.Services
{
    public class GuiaApp : IGuiaApp
    {
        private readonly IGuiaService _guiaService;

        public void AtribuirNomeAuditorUltimaModificacao(Guia model)
        {
        }

        public void AtribuirNomePrestadorUltimaModificacao(Guia model)
        {
        }
    }
}
