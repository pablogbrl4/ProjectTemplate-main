using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class DadosAuditorRepository : IDadosAuditorRepository
    {
        private readonly DativaDbContext _dativaDbContext;

        public DadosAuditorRepository(DativaDbContext dativaDbContext)
        {
            _dativaDbContext = dativaDbContext;
        }

        public DadosAuditor GetDadosAuditorByIdLogin(int idlogin)
        {
            if (_dativaDbContext.Connection.State == System.Data.ConnectionState.Closed)
                _dativaDbContext.Connection.Open();

            return _dativaDbContext.Connection.QueryFirstOrDefault<DadosAuditor>(
                    sql: dadosAuditorByLoginQuery,
                    param: new { idlogin },
                    commandType: System.Data.CommandType.Text
                );
        }

        private readonly string dadosAuditorByLoginQuery =
            $@"SELECT 
                [id_login] AS [{nameof(DadosAuditor.IdLogin)}]
                ,[ds_login] AS [{nameof(DadosAuditor.Login)}]
                ,[ds_usuario] AS [{nameof(DadosAuditor.Nome)}]
            FROM [TB_FRA_LOGIN] WITH (NOLOCK)
            WHERE [id_login] = @idLogin";
    }
}
