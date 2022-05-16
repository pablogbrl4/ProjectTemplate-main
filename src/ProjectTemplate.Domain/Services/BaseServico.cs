using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class BaseServico<T> : IBaseServico<T> where T : class
    {
        protected readonly IBaseRepositorio _repositorio;

        public BaseServico(IBaseRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
    }
}
