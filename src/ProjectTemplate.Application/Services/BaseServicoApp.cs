using AutoMapper;
using Orizon.Rest.Chat.Application.DTOs;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using Orizon.Rest.Chat.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.Application.Services
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

        public async Task IniciarTransaction()
        {
            await _service.IniciarTransaction();
        }

        public async Task SalvarMudancas(bool commit = true)
        {
            await _service.SalvarMudancas(commit);
        }

        #region Leitura

        public virtual async Task<TDTO> BuscarPorId(object id, string[] includes = default, bool tracking = false)
        {
            return _mapper.Map<TDTO>(await _service.BuscarPorId(id, includes, tracking));
        }

        public virtual async Task<TDTO> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            return _mapper.Map<TDTO>(await _service.BuscarComPesquisa(expression, includes, tracking));
        }

        public virtual async Task<IEnumerable<TDTO>> BuscarTodos(string[] includes = default, bool tracking = false)
        {
            return _mapper.Map<IEnumerable<TDTO>>(await _service.BuscarTodos(includes, tracking));
        }

        public virtual async Task<IEnumerable<TDTO>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            return _mapper.Map<IEnumerable<TDTO>>(await _service.BuscarTodosComPesquisa(expression, includes, tracking));
        }

        public virtual async Task<PaginacaoModel<TDTO>> BuscarTodosPaginacao(Expression<Func<T, bool>> expression, int limit, int page, CancellationToken cancellationToken, string[] includes = default, bool tracking = false)
        {
            return _mapper.Map<PaginacaoModel<TDTO>>(await _service.BuscarTodosPaginacao(expression, limit, page, cancellationToken, includes, tracking));
        }

        #endregion

        #region Escrita

        public virtual async Task<object> Incluir(TDTO entidade)
        {
            return await _service.Incluir(_mapper.Map<T>(entidade));
        }

        public virtual async Task IncluirLista(IEnumerable<TDTO> entidades)
        {
            await _service.IncluirLista(_mapper.Map<IEnumerable<T>>(entidades));
        }

        public virtual void Alterar(TDTO entidade)
        {
            _service.Alterar(_mapper.Map<T>(entidade));
        }

        public virtual void AlterarLista(IEnumerable<TDTO> entidades)
        {
            _service.AlterarLista(_mapper.Map<IEnumerable<T>>(entidades));
        }

        public virtual async Task Excluir(object id)
        {
            await _service.Excluir(id);
        }

        public virtual void Excluir(T entidade)
        {
            _service.Excluir(entidade);
        }

        #endregion
    }
}
