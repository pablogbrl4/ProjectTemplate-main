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
        Task IniciarTransaction();
        Task SalvarMudancas(bool commit = true);

        #region Escrita
        Task<object> Incluir(TDTO entidade);

        Task IncluirLista(IEnumerable<TDTO> entidades);

        void Alterar(TDTO entidade);

        void AlterarLista(IEnumerable<TDTO> entidades);

        Task Excluir(object id);

        void Excluir(T entidade);
        #endregion

        #region Leitura
        Task<TDTO> BuscarPorId(object id, string[] includes = default, bool tracking = false);

        Task<TDTO> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false);

        Task<IEnumerable<TDTO>> BuscarTodos(string[] includes = default, bool tracking = false);

        Task<IEnumerable<TDTO>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false);

        Task<PaginacaoModel<TDTO>> BuscarTodosPaginacao(Expression<Func<T, bool>> expression, int limit, int page, CancellationToken cancellationToken, string[] includes = default, bool tracking = false);
        #endregion
    }
}
