using System.Data;

namespace Orizon.Rest.Chat.Infra.Data.Provider
{
    public interface IDbConnectionProvider
    {
        public IDbConnection GetConnection(string connectionString);
    }
}
