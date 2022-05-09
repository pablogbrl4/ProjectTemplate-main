using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.Domain.Interfaces.Repositories
{
    public interface IBaseRepositorio<T> where T : BaseEntidade
    {
        Task IniciarTransaction();

        Task SalvarMudancas(bool commit = true);

        #region Escrita

        Task<object> Incluir(T entidade);

        Task IncluirLista(IEnumerable<T> entidades);

        void Alterar(T entidade);

        void AlterarLista(IEnumerable<T> entidades);

        Task Excluir(object id);

        void Excluir(T entidade);

        #endregion Escrita

        #region Leitura

        Task<T> BuscarPorId(object id, string[] includes = default, bool tracking = false);

        Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false);

        Task<IEnumerable<T>> BuscarTodos(string[] includes = default, bool tracking = false);

        Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false);

        Task<PaginacaoModel<T>> BuscarTodosPaginacao(Expression<Func<T, bool>> expression, int limit,int page,CancellationToken cancellationToken,string[] includes = default,bool tracking = false);

        #endregion Leitura
    }
}
