using Microsoft.EntityFrameworkCore;
using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Interfaces.Repositories;
using ProjectTemplate.Domain.Paginacao;
using ProjectTemplate.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;


namespace ProjectTemplate.Infra.Data.Repositories
{
    public class BaseRepositorio<T> : IBaseRepositorio<T> where T : BaseEntidade
    {
        protected readonly BaseContexto _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepositorio(BaseContexto context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #region Leitura
        public IQueryable<T> Buscar(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            var query = _dbSet.Where(expression);
            if (tracking == false)
                query = query.AsNoTracking();
            if (includes != null)
                foreach (var property in includes)
                    query = query.Include(property);
            return query;
        }

        public virtual async Task<T> BuscarPorId(Guid id, string[] includes = default, bool tracking = false)
        {
            return await Buscar(x => x.Id == id, includes, tracking).FirstOrDefaultAsync();
        }

        public virtual async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true)
        {
            return await Buscar(expression, includes).FirstOrDefaultAsync();
        }
       
        public async Task<IEnumerable<T>> BuscarTodos(string[] includes = default, bool tracking = true)
        {
            return await Buscar(x => true, includes).ToListAsync();
        }

        public async Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = true)
        {
            return await Buscar(expression, includes).ToListAsync();
        }

        public async Task<PaginacaoModel<T>> BuscarTodosPaginacao(int limit, int page, CancellationToken cancellationToken, string[] includes = default)
        {
            var entidades = await _dbSet
                      .AsNoTracking()
                      .PaginateAsync(page, limit, cancellationToken, includes);

            var entidadesPaginacao = new PaginacaoModel<T>
            {
                PaginaAtual = entidades.CurrentPage,
                TotalItens = entidades.TotalItems,
                TotalPaginas = entidades.TotalPages,
                Itens = entidades.Items.ToList()
            };

            return entidadesPaginacao;
        }

        #endregion

        #region Escrita

        public virtual async Task<Guid> Incluir(T entidade)
        {
            var obj = await _dbSet.AddAsync(entidade);
            return obj.Entity.Id;
        }

        public virtual async Task IncluirLista(List<T> entidades)
        {
            await _dbSet.AddRangeAsync(entidades);
        }

        public virtual void Alterar(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
        }

        public async Task<bool> Excluir(Guid id)
        {
            try
            {
                var entidade = await BuscarPorId(id);
                if (entidade != null)
                {
                    _dbSet.Remove(entidade);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task IniciarTransaction()
        {
            await _context.IniciarTransaction();
        }

        public async Task SalvarMudancas(bool commit = true)
        {
            await _context.SalvarMudancas(commit);
        }

        #endregion
    }
}
