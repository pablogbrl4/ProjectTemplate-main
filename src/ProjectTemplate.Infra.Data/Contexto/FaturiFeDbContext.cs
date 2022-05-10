using Orizon.Rest.Chat.Infra.Data.Provider;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Contexto
{
    [ExcludeFromCodeCoverage]
    public class FaturiFeDbContext
    {
        public IDbConnection Connection { get; }

        public FaturiFeDbContext(SqlConnectionProvider sqlConnectionProvider, string connectionString)
        {
            Connection = sqlConnectionProvider.GetConnection(connectionString);
        }
    }
}
