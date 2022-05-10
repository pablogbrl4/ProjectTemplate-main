using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Provider
{
    [ExcludeFromCodeCoverage]
    public class SqlConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
