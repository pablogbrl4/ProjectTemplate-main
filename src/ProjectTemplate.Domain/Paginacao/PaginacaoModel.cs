using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Paginacao
{
    public class PaginacaoModel<TModel> : ILinkedResource
    {
        public int PaginaAtual { get; init; }

        public int TotalItens { get; init; }

        public int TotalPaginas { get; init; }

        public List<TModel> Itens { get; init; }

        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }

    }
}
