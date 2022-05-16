using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]

    public class DadosAuditorRepository : BaseRepositorio, IDadosAuditorRepository
    {
        public DadosAuditorRepository(PrefatDbContext prefatDbContext, DativaDbContext dativaDbContext)
            : base(prefatDbContext, dativaDbContext)
        {
        }

        public DadosAuditor GetDadosAuditorByIdLogin(int idlogin)
        {
            OpenConnectionDativa();
            return _dativaDbContext.Connection.QueryFirstOrDefault<DadosAuditor>(
                    sql: dadosAuditorByLoginQuery,
                    param: new { idlogin },
                    commandType: System.Data.CommandType.Text
                );
        }

        private readonly string dadosAuditorByLoginQuery =
            $@"
              SELECT
                  [id_login] AS [{nameof(DadosAuditor.IdLogin)}],
                  [ds_login] AS [{nameof(DadosAuditor.Login)}],
                  [ds_usuario] AS [{nameof(DadosAuditor.Nome)}]
              FROM
                  [TB_FRA_LOGIN] WITH (NOLOCK)
              WHERE
                  [id_login] = @idLogin
            ";
    }
}
