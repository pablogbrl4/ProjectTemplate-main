using Orizon.Rest.Chat.Infra.Data.Contexto;
using System;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseContexto _context;
        private bool disposed = false;

        public UnitOfWork(BaseContexto baseContexto)
        {
            this._context = baseContexto;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public async Task IniciarTransaction()
        {
            await _context.IniciarTransaction();
        }

        public async Task SalvarMudancas(bool commit = true)
        {
            await _context.SalvarMudancas(commit);
        }
    }
}
