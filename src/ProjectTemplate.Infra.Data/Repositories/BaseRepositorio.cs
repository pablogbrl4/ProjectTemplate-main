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

        public async Task<IQueryable<T>> Buscar(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            var query = _dbSet.Where(expression);
            if (includes != null)
                foreach (var property in includes)
                    query = query.Include(property);
            return query;
        }

        public async Task<T> BuscarPorId(int id, string[] includes = default)
        {
            return await Buscar(x => x.Id == id, includes).Result.FirstOrDefaultAsync();
        }

        public async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return await Buscar(expression, includes).Result.FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<T>> BuscarTodos(string[] includes = default)
        {
            return await Buscar(x => true, includes).Result.ToListAsync();
        }

        public async Task<IEnumerable<T>> BuscarTodosComPesquisa(Expression<Func<T, bool>> expression, string[] includes = default)
        {
            return await Buscar(expression, includes).Result.ToListAsync();
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

        public async Task<int> Incluir(T entidade)
        {
            await _context.IniciarTransaction();
            var obj = await _dbSet.AddAsync(entidade);
            await _context.SalvarMudancas();
            return obj.Entity.Id;
        }

        public async Task<List<T>> IncluirLista(List<T> entidade)
        {
            await _context.IniciarTransaction();
            await _dbSet.AddRangeAsync(entidade);
            await _context.SalvarMudancas();

            return entidade;
        }

        public virtual async Task<T> Alterar(T entidade)
        {
            await _context.IniciarTransaction();
            _context.Entry(entidade).State = EntityState.Modified;
            await _context.SalvarMudancas();
            return entidade;
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var entidade = await BuscarPorId(id);
                if (entidade != null)
                {
                    await _context.IniciarTransaction();
                    _dbSet.Remove(entidade);
                    await _context.SalvarMudancas();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
