using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Domain.Services
{
    public class BaseServico<T> : IBaseServico<T> where T : class
    {
        protected readonly IBaseRepositorio<T> _repositorio;

        public BaseServico(IBaseRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }
    }
}
