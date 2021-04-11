using System.Collections.Generic;

namespace ProjectTemplate.Domain.Paginacao
{
    public interface ILinkedResource
    {
        public IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
