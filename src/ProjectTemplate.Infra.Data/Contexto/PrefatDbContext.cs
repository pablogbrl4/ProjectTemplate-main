using Orizon.Rest.Chat.Infra.Data.Provider;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Contexto
{
    [ExcludeFromCodeCoverage]
    public class PrefatDbContext
    {
        public IDbConnection Connection { get; }

        public PrefatDbContext(SqlConnectionProvider sqlConnectionProvider, string connectionString)
        {
            Connection = sqlConnectionProvider.GetConnection(connectionString);
        }
    }
}
