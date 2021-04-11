using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Interfaces.Repositories
{
    public interface IBaseRepositorio<T> where T : BaseEntidade
    {
        #region Escrita
        Task<int> Incluir(T entidade);

        Task<List<T>> IncluirLista(List<T> entidade);

        Task<T> Alterar(T entidade);

        Task<bool> Excluir(int id);
        #endregion

        #region Leitura
        Task<T> BuscarPorId(int id, string[] includes = default);

        Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default);

        Task<IQueryable<T>> Buscar(Expression<Func<T, bool>> expression, string[] includes = default);

        Task<IEnumerable<T>> BuscarTodos(string[] includes = default);

        Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes);

        Task<PaginacaoModel<T>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = default);

        #endregion
    }
}
