using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System;
using System.Data;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class ApontamentoRepository : IApontamentoRepository
    {
        private readonly PrefatDbContext _prefatDbContext;

        public ApontamentoRepository(PrefatDbContext prefatDbContext)
        {
            _prefatDbContext = prefatDbContext;
        }

        public void InserirDados(Mensagem[] mensagens)
        {
            return;

            /*

            var bancoDados = new BancoDados("PreFaturamento")
            {
                TipoComando = CommandType.StoredProcedure
            };

            try
            {
                var dadosAuditor = _dadosAuditorDao.GetDadosAuditorByIdLogin(mensagens[0].IdLoginRemetente);

                bancoDados.BeginTransaction();
                foreach (var msg in mensagens)
                {
                    msg.DsLoginRemetente = dadosAuditor?.Nome ?? msg.DsLoginRemetente;

                    int idChat = InserirDadosConversa(bancoDados, msg);
                    AtualizaIdChat(bancoDados, msg, idChat);
                }
                bancoDados.TransactionCommit();
            }
            catch
            {
                bancoDados.TransactionRollback();
                throw;
            }
            finally
            {
                bancoDados.Close();
            }

            */
        }

        /*
         * 
        private int InserirDadosConversa(BancoDados bancoDados, Mensagem msg)
        {
            return 0;
            
            bancoDados.TextoComando = "PRC_CHAT_INSERIR_DADOS_APONTAMENTO";
            bancoDados.LimparParametros();
            bancoDados.CriarParametro("ID_LOGIN", DbType.Int32, msg.IdLoginRemetente);
            bancoDados.CriarParametro("CONVERSA", DbType.String, msg.Conversa);
            bancoDados.CriarParametro("DS_LOGIN", DbType.String, msg.DsLoginRemetente);
            bancoDados.CriarParametro("ID_CHAT", DbType.Int32, msg.FkChat);
            var idChat = Convert.ToInt32(bancoDados.ExecutarEscalar());
            return idChat;
            
        }

        private void AtualizaIdChat(BancoDados bancoDados, Mensagem msg, int idChat)
        {
            bancoDados.TextoComando = "PROC_UPDATE_ITEM_CHAT";
            bancoDados.LimparParametros();
            bancoDados.CriarParametro("ID_ITEM", DbType.Int32, msg.Id);
            bancoDados.CriarParametro("ID_CHAT", DbType.Int32, idChat);
            bancoDados.ExecutarComando();
        }

        */
    }
}
