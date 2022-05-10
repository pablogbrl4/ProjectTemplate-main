using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class GuiaRepository : IGuiaRepository
    {
        private readonly PrefatDbContext _prefatDbContext;

        public GuiaRepository(PrefatDbContext prefatDbContext)
        {
            _prefatDbContext = prefatDbContext;
        }

        private void AbriConexao()
        {
            if (_prefatDbContext.Connection.State == System.Data.ConnectionState.Closed)
                _prefatDbContext.Connection.Open();
        }

        private const string AtualizarGuiaAuditorQuery = 
            @" 
            UPDATE CONTA SET 
            [ULTIMO_LOGIN_AUDITOR] = @ULTIMO_LOGIN 
            WHERE [ID_CONTA] = @ID_CONTA 
            ";

        private const string AtualizarGuiaPrestadorQuery = 
            @" 
            UPDATE CONTA SET 
            [ULTIMO_LOGIN_PRESTADOR] = @ULTIMO_LOGIN 
            WHERE [ID_CONTA] = @ID_CONTA 
            ";

        /// <summary>
        /// Atualiza o ultimo usuario do auditor
        /// </summary>
        /// <param name="model"></param>
        public void AtribuirNomeAuditorUltimaModificacao(Guia model)
        {
            AbriConexao();

            _prefatDbContext.Connection.Execute(
                  sql: AtualizarGuiaAuditorQuery,
                  param: new { model.ULTIMO_LOGIN, model.ID_CONTA },
                  commandType: System.Data.CommandType.Text
                );
        }

        /// <summary>
        /// Atualiza o ultimo usuario do prestador
        /// </summary>
        /// <param name="model"></param>
        public void AtribuirNomePrestadorUltimaModificacao(Guia model)
        {
            AbriConexao();

            _prefatDbContext.Connection.Execute(
                  sql: AtualizarGuiaPrestadorQuery,
                  param: new { model.ULTIMO_LOGIN, model.ID_CONTA },
                  commandType: System.Data.CommandType.Text
                );
        }
    }
}
