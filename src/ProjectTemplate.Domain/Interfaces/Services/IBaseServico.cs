using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Interfaces.Services
{
    public interface IBaseServico<T> where T : BaseEntidade
    {
        #region Escrita
        Task<Guid> Incluir(T entidade);

        Task IncluirLista(List<T> entidade);

        Task Alterar(T entidade);

        Task<bool> Excluir(Guid id);
        #endregion

        #region Leitura
        IQueryable<T> Buscar(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true);

        Task<T> BuscarPorId(Guid id, string[] includes = default, bool tracking = true);

        Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true);

        Task<IEnumerable<T>> BuscarTodos(string[] includes = default, bool tracking = true);

        Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes, bool tracking = true);

        Task<PaginacaoModel<T>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = default);
        #endregion
    }
}
