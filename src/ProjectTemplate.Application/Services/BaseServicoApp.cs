using AutoMapper;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Interfaces.Services;

namespace Orizon.Rest.Chat.Application.Services
{
    public class BaseServicoApp<T, TDTO> : IBaseApp<T, TDTO>
         where T : class
         where TDTO : class
    {
        protected readonly IBaseServico<T> _service;
        protected readonly IMapper _mapper;

        public BaseServicoApp(
            IBaseServico<T> service,
            IMapper mapper) : base()
        {
            _service = service;
            _mapper = mapper;
        }

    }
}
