using AutoMapper;
using ProjectTemplate.Application.DTOs;
using ProjectTemplate.Application.Interfaces;
using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Interfaces.Services;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTemplate.Application.Services
{
    public class BaseServicoApp<T, TDTO> : IBaseApp<T, TDTO>
         where T : BaseEntidade
         where TDTO : BaseEntidadeDTO
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

        #region Leitura
        public async Task<TDTO> BuscarPorId(Guid id)
        {
            return _mapper.Map<TDTO>(await _service.BuscarPorId(id));
        }

        public async Task<IEnumerable<TDTO>> BuscarTodos(string[] includes = default)
        {
            return _mapper.Map<IEnumerable<TDTO>>(await _service.BuscarTodos(includes));
        }

        public async Task<TDTO> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return _mapper.Map<TDTO>(await _service.BuscarComPesquisa(expression, includes));
        }

        public async Task<IEnumerable<TDTO>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return _mapper.Map<IEnumerable<TDTO>>(await _service.BuscarTodosComPesquisa(expression, includes));
        }

        public async Task<PaginacaoModel<TDTO>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = null)
        {
            return _mapper.Map<PaginacaoModel<TDTO>>(await _service.BuscarTodosPaginacao(limit, page, cancellationToken, includes));
        }

        #endregion

        #region Escrita

        public async Task<Guid> Incluir(TDTO entidade)
        {
            return await _service.Incluir(_mapper.Map<T>(entidade));
        }

        public async Task IncluirLista(List<TDTO> entidade)
        {
            await _service.IncluirLista(_mapper.Map<List<T>>(entidade));
        }

        public async Task Alterar(TDTO entidade)
        {
            await _service.Alterar(_mapper.Map<T>(entidade));
        }

        public async Task<bool> Excluir(Guid id)
        {
            return await _service.Excluir(id);
        }

        #endregion

    }
}
