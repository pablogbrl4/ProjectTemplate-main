using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Interfaces.Repositories;
using ProjectTemplate.Domain.Interfaces.Services;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> Incluir(T entidade)
        {
            return await _repositorio.Incluir(entidade);
        }

        public async Task<List<T>> IncluirLista(List<T> entidade)
        {
            return await _repositorio.IncluirLista(entidade);
        }

        public virtual async Task<T> Alterar(T entidade)
        {
            return await _repositorio.Alterar(entidade);
        }

        public async Task<bool> Excluir(int id)
        {
            return await _repositorio.Excluir(id);
        }

        #endregion

        #region Leitura
        public virtual async Task<T> BuscarPorId(int id, string[] includes = default)
        {
            return await _repositorio.BuscarPorId(id, includes);
        }

        public async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return await _repositorio.BuscarComPesquisa(expression);
        }

        public Task<IQueryable<T>> Buscar(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return _repositorio.Buscar(expression);
        }

        public virtual async Task<IEnumerable<T>> BuscarTodos(string[] includes = default)
        {
            return await _repositorio.BuscarTodos(includes);
        }

        public async Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return await _repositorio.BuscarTodosComPesquisa(expression, includes);
        }

        public async Task<PaginacaoModel<T>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = null)
        {
            return await _repositorio.BuscarTodosPaginacao(limit, page, cancellationToken, includes);
        }
        #endregion
    }
}
