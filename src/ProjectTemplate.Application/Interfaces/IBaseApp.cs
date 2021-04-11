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
        Task<int> Incluir(TDTO entidade);

        Task<List<TDTO>> IncluirLista(List<TDTO> entidade);

        Task<TDTO> Alterar(TDTO entidade);

        Task<bool> Excluir(int id);
        #endregion


        #region Leitura
        Task<TDTO> BuscarPorId(int id);

        Task<IEnumerable<TDTO>> BuscarTodos(string[] includes = default);

        Task<TDTO> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default);

        Task<IEnumerable<TDTO>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default);

        Task<PaginacaoModel<TDTO>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = default);
        #endregion
    }
}
