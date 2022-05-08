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

        public async Task IniciarTransaction()
        {
            await _context.IniciarTransaction();
        }

        public async Task SalvarMudancas(bool commit = true)
        {
            await _context.SalvarMudancas(commit);
        }

        #region Leitura

        private IQueryable<T> Buscar(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            var query = _dbSet.Where(expression);
            if (tracking == false)
                query = query.AsNoTracking();
            if (includes != null)
                foreach (var property in includes)
                    query = query.Include(property);
            return query;
        }

        public virtual async Task<T> BuscarPorId(object id, string[] includes = default, bool tracking = false)
        {
            return await Buscar(x => x.Id == id, includes, tracking).FirstOrDefaultAsync();
        }

        public virtual async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            return await Buscar(expression, includes).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> BuscarTodos(string[] includes = default, bool tracking = false)
        {
            return await Buscar(x => true, includes).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default, bool tracking = false)
        {
            return await Buscar(expression, includes).ToListAsync();
        }

        public virtual async Task<PaginacaoModel<T>> BuscarTodosPaginacao(Expression<Func<T, bool>> expression, int limit, int page, CancellationToken cancellationToken, string[] includes = default, bool tracking = false)
        {
            PagedModel<T> entidades;

            if (tracking)
                entidades = await _dbSet.Where(expression).PaginateAsync(page, limit, cancellationToken, includes);
            else
                entidades = await _dbSet.Where(expression).AsNoTracking().PaginateAsync(page, limit, cancellationToken, includes);

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

        public virtual async Task<object> Incluir(T entidade)
        {
            var obj = await _dbSet.AddAsync(entidade);
            return obj.Entity.Id;
        }

        public virtual async Task IncluirLista(IEnumerable<T> entidades)
        {
            await _dbSet.AddRangeAsync(entidades);
        }

        public virtual void Alterar(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
        }

        public virtual void AlterarLista(IEnumerable<T> entidades)
        {
            foreach (var entidade in entidades)
            {
                _context.Entry(entidade).State = EntityState.Modified;
            }
        }

        public virtual async Task Excluir(object id)
        {
            var entidade = await BuscarPorId(id);
            if (entidade != null)
                _dbSet.Remove(entidade);
        }

        public virtual void Excluir(T entidade)
        {
            _dbSet.Remove(entidade);
        }

        #endregion
    }
}
