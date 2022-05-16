using Dapper;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System.Data;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class ChatLeituraRepository : BaseRepositorio, IChatLeituraRepository
    {
        public ChatLeituraRepository(PrefatDbContext prefatDbContext, DativaDbContext dativaDbContext)
            : base(prefatDbContext, dativaDbContext)
        {
        }

        public int NaoLidas(int idChat, int idLogin)
        {
            OpenConnectionPrefat();
            var sql =
                $@"
                  SELECT
                      COUNT(1)
                  FROM
                      CHAT_CONVERSAS WITH (NOLOCK)
                  WHERE
                      NOT EXISTS (
                          SELECT
                              1
                          FROM
                              CHAT_LEITURA WITH (NOLOCK)
                          WHERE
                              FK_CHAT_CONVERSAS = ID_CHAT_CONVERSAS
                              AND ID_LOGIN = {idLogin}
                      )
                      AND FK_CHAT = {idChat}
                ";
            return _prefatDbContext.Connection.ExecuteScalar<int>(
                    sql: sql,
                    commandType: CommandType.Text
                );
        }
    }
}
