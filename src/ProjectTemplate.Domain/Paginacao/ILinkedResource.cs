using System.Collections.Generic;

namespace Orizon.Rest.Chat.Domain.Paginacao
{
    public interface ILinkedResource
    {
        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
