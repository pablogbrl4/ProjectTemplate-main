using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class GuiaService : IGuiaService
    {
        private readonly IGuiaRepository _guiaRepository;

        public GuiaService(IGuiaRepository guiaRepository)
        {
            _guiaRepository = guiaRepository;
        }

        public void AtribuirNomeAuditorUltimaModificacao(Guia model)
        {
            _guiaRepository.AtribuirNomeAuditorUltimaModificacao(model);
        }

        public void AtribuirNomePrestadorUltimaModificacao(Guia model)
        {
            _guiaRepository.AtribuirNomePrestadorUltimaModificacao(model);
        }
    }
}
