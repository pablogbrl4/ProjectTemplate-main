using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Infra.Data.Contexto
{
    public class BaseContexto : DbContext
    {
        protected IDbContextTransaction _contextoTransaction { get; set; }

        public BaseContexto(DbContextOptions<BaseContexto> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public async Task<IDbContextTransaction> IniciarTransaction()
        {
            if (_contextoTransaction == null)
                _contextoTransaction = await this.Database.BeginTransactionAsync();

            return _contextoTransaction;
        }

        public async Task SalvarMudancas(bool commit = true)
        {
            await Salvar();
            if (commit)
                await Commit();
        }

        private async Task Salvar()
        {
            try
            {
                ChangeTracker.DetectChanges();
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await RollBack();
                throw new Exception(ex.Message);
            }
        }

        private async Task RollBack()
        {
            if (_contextoTransaction != null)
                await _contextoTransaction.RollbackAsync();
        }

        private async Task Commit()
        {
            if (_contextoTransaction != null)
            {
                await _contextoTransaction.CommitAsync();
                await _contextoTransaction.DisposeAsync();
                _contextoTransaction = null;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new EntityMap());
        }
    }
}
