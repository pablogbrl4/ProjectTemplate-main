using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]

    public class CicloAuditoriaRepository : ICicloAuditoriaRepository
    {
        private readonly PrefatDbContext _prefatDbContext;

        public CicloAuditoriaRepository(PrefatDbContext prefatDbContext)
        {
            _prefatDbContext = prefatDbContext;
        }

        public CicloAuditoria GetCicloDadosApontamento(int idItem)
        {
            if (_prefatDbContext.Connection.State == System.Data.ConnectionState.Closed)
                _prefatDbContext.Connection.Open();

            return _prefatDbContext.Connection.QueryFirstOrDefault<CicloAuditoria>(
                    sql: cicloDadosApontamentoQuery,
                    param: new { idItem },
                    commandType: System.Data.CommandType.Text
                );
        }

        private readonly string cicloDadosApontamentoQuery =
            $@"SELECT 
            	ISNULL(C.QTD_CICLO_AUDIT,0) AS [{nameof(CicloAuditoria.ciclo)}]
            	,C.ID_LOTE AS [{nameof(CicloAuditoria.protocolo)}]
            	,D.QUANTIDADE AS [{nameof(CicloAuditoria.quantidade)}]
            	,D.VALOR_UNIDADE AS [{nameof(CicloAuditoria.valorUnidade)}]
                ,D.OBSERVACAO AS [{nameof(CicloAuditoria.observacao)}]
            	,I.DESCRICAO AS [{nameof(CicloAuditoria.descricaoItem)}]
            FROM CONTA C
            INNER JOIN ITEM I
            ON I.ID_CONTA= C.ID_CONTA
            OUTER APPLY
            (
            	SELECT TOP 1 ITEM_DETALHE.QUANTIDADE, ITEM_DETALHE.VALOR_UNIDADE, ITEM_DETALHE.OBSERVACAO FROM ITEM_DETALHE WHERE ITEM_DETALHE.FK_ITEM = I.ID_ITEM ORDER BY ID_ITEM_DETALHE DESC
            ) D
            WHERE I.ID_ITEM = @idItem";
    }
}
