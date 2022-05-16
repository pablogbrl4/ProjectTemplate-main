using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System.Data;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class BaseRepositorio : IBaseRepositorio
    {
        protected readonly PrefatDbContext _prefatDbContext;
        protected readonly DativaDbContext _dativaDbContext;

        protected IDbTransaction? TransactionPrefat { get; set; }

        public BaseRepositorio(PrefatDbContext prefatDbContext, DativaDbContext dativaDbContext)
        {
            _prefatDbContext = prefatDbContext;
            _dativaDbContext = dativaDbContext;
        }

        protected void OpenConnectionPrefat()
        {
            if (_prefatDbContext.Connection.State == ConnectionState.Closed)
                _prefatDbContext.Connection.Open();
        }

        protected void OpenConnectionDativa()
        {
            if (_dativaDbContext.Connection.State == ConnectionState.Closed)
                _dativaDbContext.Connection.Open();
        }

        protected void BeginTransactionPrefat()
        {
            OpenConnectionPrefat();
            TransactionPrefat = _prefatDbContext.Connection.BeginTransaction();
        }

        protected void CommitPrefat()
        {
            try
            {
                if (TransactionPrefat != null)
                    TransactionPrefat.Commit();
            }
            catch
            {
                if (TransactionPrefat != null)
                    TransactionPrefat.Rollback();
                throw;
            }
        }
    }
}
