using ProjectTemplate.Application.DTOs;
using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTemplate.Application.Interfaces
{
    public interface IBaseApp<T, TDTO> where T : BaseEntidade where TDTO : BaseEntidadeDTO
    {
        #region Escrita
        Task<Guid> Incluir(TDTO entidade);

        Task IncluirLista(List<TDTO> entidade);

        Task Alterar(TDTO entidade);

        Task<bool> Excluir(Guid id);
        #endregion

        #region Leitura
        Task<TDTO> BuscarPorId(Guid id);

        Task<IEnumerable<TDTO>> BuscarTodos(string[] includes = default);

        Task<TDTO> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default);

        Task<IEnumerable<TDTO>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default);

        Task<PaginacaoModel<TDTO>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = default);
        #endregion
    }
}
