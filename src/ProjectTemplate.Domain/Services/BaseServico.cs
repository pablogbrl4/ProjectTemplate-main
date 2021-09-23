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
        public virtual async Task<Guid> Incluir(T entidade)
        {
            await _repositorio.IniciarTransaction();
            Guid id = await _repositorio.Incluir(entidade);
            await _repositorio.SalvarMudancas();
            return id;
        }

        public virtual async Task IncluirLista(List<T> entidade)
        {
            await _repositorio.IniciarTransaction();
            await _repositorio.IncluirLista(entidade);
            await _repositorio.SalvarMudancas();
        }

        public virtual async Task Alterar(T entidade)
        {
            await _repositorio.IniciarTransaction();
             _repositorio.Alterar(entidade);
            await _repositorio.SalvarMudancas();
        }

        public async Task<bool> Excluir(Guid id)
        {
            return await _repositorio.Excluir(id);
        }

        #endregion

        #region Leitura

        public IQueryable<T> Buscar(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true)
        {
            return _repositorio.Buscar(expression, includes, tracking);
        }

        public virtual async Task<T> BuscarPorId(Guid id, string[] includes = default, bool tracking = true)
        {
            return await _repositorio.BuscarPorId(id, includes, tracking);
        }

        public virtual async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true)
        {
            return await _repositorio.BuscarComPesquisa(expression, includes, tracking);
        }

        public virtual async Task<IEnumerable<T>> BuscarTodos(string[] includes = default, bool tracking = true)
        {
            return await _repositorio.BuscarTodos(includes, tracking);
        }

        public virtual async Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true)
        {
            return await _repositorio.BuscarTodosComPesquisa(expression, includes, tracking);
        }

        public async Task<PaginacaoModel<T>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = null)
        {
            return await _repositorio.BuscarTodosPaginacao(limit, page, cancellationToken, includes);
        }
        #endregion
    }
}
