using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Interfaces.Repositories;
using ProjectTemplate.Domain.Interfaces.Services;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Services
{
    public class BaseServico<T> : IBaseServico<T> where T : BaseEntidade
    {
        protected readonly IBaseRepositorio<T> _repositorio;

        public BaseServico(IBaseRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }

        #region Escrita

        public virtual async Task<object> Incluir(T entidade)
        {
            return await _repositorio.Incluir(entidade);
        }

        public virtual async Task IncluirLista(IEnumerable<T> entidades)
        {
            await _repositorio.IncluirLista(entidades);
        }

        public virtual void Alterar(T entidade)
        {
            _repositorio.Alterar(entidade);
        }
        public virtual void AlterarLista(IEnumerable<T> entidades)
        {
            _repositorio.AlterarLista(entidades);
        }

        public virtual async Task Excluir(object id)
        {
            await _repositorio.Excluir(id);
        }

        public virtual void Excluir(T entidade)
        {
            _repositorio.Excluir(entidade);
        }

        #endregion

        #region Leitura

        public virtual async Task<T> BuscarPorId(object id, string[] includes = default, bool tracking = false)
        {
            return await _repositorio.BuscarPorId(id, includes, tracking);
        }

        public virtual async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            return await _repositorio.BuscarComPesquisa(expression, includes, tracking);
        }

        public virtual async Task<IEnumerable<T>> BuscarTodos(string[] includes = default, bool tracking = false)
        {
            return await _repositorio.BuscarTodos(includes, tracking);
        }

        public virtual async Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            return await _repositorio.BuscarTodosComPesquisa(expression, includes, tracking);
        }

        public async Task<PaginacaoModel<T>> BuscarTodosPaginacao(Expression<Func<T, bool>> expression, int limit, int page, CancellationToken cancellationToken, string[] includes = default, bool tracking = false)
        {
            return await _repositorio.BuscarTodosPaginacao(expression, limit, page, cancellationToken, includes, tracking);
        }
        #endregion
    }
}
