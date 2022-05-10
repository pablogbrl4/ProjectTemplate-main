using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class ChatConversasRepository : IChatConversasRepository
    {
        private static readonly string AuditorTitulo = "Analista";

        private readonly PrefatDbContext _prefatDbContext;

        public ChatConversasRepository(PrefatDbContext prefatDbContext)
        {
            _prefatDbContext = prefatDbContext;
        }

        /// <summary>
        /// Inserir Chat Mensagem
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="origem"></param>
        public void Insert(Mensagem mensagem, string origem)
        {
            return;

            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_insert_chat_conversas,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, mensagem.FkChat);
            bd.CriarParametro("CONVERSA", DbType.String, mensagem.Conversa);
            bd.CriarParametro("ID_LOGIN_REMETENTE", DbType.Int32, mensagem.IdLoginRemetente);
            bd.CriarParametro("DS_LOGIN_REMETENTE", DbType.String, mensagem.DsLoginRemetente);
            bd.CriarParametro("ORIGEM", DbType.String, origem);
            bd.ExecutarComando();

            */
        }

        /// <summary>
        /// Atualizar Chat Mensagem
        /// </summary>
        /// <param name="IdChatConversas"></param>
        /// <param name="fkchat"></param>
        /// <param name="conversa"></param>
        public void AtualizarMensagemChat(int fkchat, int IdChatConversas, string conversa)
        {
            return;

            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_update_chat_conversa,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, fkchat);
            bd.CriarParametro("CONVERSA", DbType.String, conversa);
            bd.CriarParametro("IdChatConversas", DbType.Int32, IdChatConversas);
            bd.ExecutarComando();
            */
        }

        /// <summary>
        /// Buscar ultima mensagem
        /// </summary>
        /// <param name="remetente"></param>
        /// <param name="FkChat"></param>
        public int BuscarUltimaMensagemChat(int FkChat, int remetente)
        {
            return 0;

            /*

            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_select_Ultima_conversa,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, FkChat);
            bd.CriarParametro("IdLoginRemetente", DbType.Int32, remetente);

            DataTable dataTable = bd.ExecutarDataTable();

            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["ID_CHAT_CONVERSAS"]);
            }

            return 0;

            */
        }

        public bool BuscarChatRemetente(int IdChatConversas, int remetente, int fkChat)
        {
            return false;
            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_select_conversa_remetente,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, fkChat);
            bd.CriarParametro("IdChatConversas", DbType.Int32, IdChatConversas);
            bd.CriarParametro("IdLoginRemetente", DbType.Int32, remetente);

            DataTable dataTable = bd.ExecutarDataTable();

            return dataTable.Rows.Count > 0;

            */
        }

        /// <summary>
        /// Atualizar Chat Mensagem
        /// </summary>
        /// <param name="IdChatConversas"></param>
        /// <param name="fkchat"></param>
        /// <param name="idRemetente"></param>
        public void DeletarChatConversas(int fkchat, int IdChatConversas, int idRemetente)
        {
            return;

            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_delete_chat_conversa,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, fkchat);
            bd.CriarParametro("IdChatConversas", DbType.Int32, IdChatConversas);
            bd.CriarParametro("IdLoginRemetente", DbType.Int32, idRemetente);
            bd.ExecutarComando();

            */
        }

        /// <summary>
        /// Listar Chat Mensagem
        /// </summary>
        /// <param name="fkChat"></param>
        /// <returns></returns>
        public IEnumerable<ChatConversas> Listar(int fkChat)
        {
            return null;

            /*
            return ListarImpl(fkChat, null);
            */
        }

        public IEnumerable<ChatConversas> Listar(int fkChat, string origem)
        {
            return null;

            /*
            return ListarImpl(fkChat, origem.Equals(OrigemRequest.Prestador) ? AuditorTitulo : null);

            */
        }

        public IEnumerable<ChatConversas> ListarPorFkChatConversa(int fkChat, string conversa)
        {
            return null;

            /*
            var lstContaConversasModel = new List<ChatConversas>();
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_select_chat_conversas_listar_por_FkChat_Conversa,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, fkChat);
            bd.CriarParametro("CONVERSA", DbType.String, conversa);

            DataTable dataTable = bd.ExecutarDataTable();

            if (dataTable.Rows.Count > 0)
            {
                lstContaConversasModel = new List<ChatConversasModel>(dataTable.Rows.Count);
                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = Bind(linha);
                    lstContaConversasModel.Add(obj);
                }
            }

            return lstContaConversasModel;

            */
        }

        /*
        private IEnumerable<ChatConversas> ListarImpl(int fkChat, string remetente)
        {
            var lstContaConversasModel = new IEnumerable<ChatConversasModel>();
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = query_select_chat_conversas_listar,
                TipoComando = CommandType.Text
            };

            bd.CriarParametro("FK_CHAT", DbType.Int32, fkChat);

            DataTable dataTable = bd.ExecutarDataTable();

            if (dataTable.Rows.Count > 0)
            {
                lstContaConversasModel = new IEnumerable<ChatConversasModel>(dataTable.Rows.Count);
                foreach (DataRow linha in dataTable.Rows)
                {
                    var obj = Bind(linha);

                    if (obj.Origem == OrigemRequest.Auditor)
                        obj.DsLoginRemetente = remetente ?? obj.DsLoginRemetente;

                    lstContaConversasModel.Add(obj);
                }
            }

            return lstContaConversasModel;
        }

        private ChatConversas Bind(DataRow oDataRow)
        {
            ChatConversasModel chatConversasModel = new ChatConversasModel();
            chatConversasModel.IdChatConversas = Convert.ToInt32(oDataRow["ID_CHAT_CONVERSAS"]);
            chatConversasModel.FkChat = Convert.ToInt32(oDataRow["FK_CHAT"]);
            chatConversasModel.Data = Converter.ConverterToDateTime(oDataRow["DATA"].ToString());
            chatConversasModel.Conversa = oDataRow["CONVERSA"].ToString();
            chatConversasModel.IdLoginRemetente = Convert.ToInt32(oDataRow["ID_LOGIN_REMETENTE"]);
            chatConversasModel.DsLoginRemetente = oDataRow["DS_LOGIN_REMETENTE"].ToString();
            chatConversasModel.Origem = oDataRow["ORIGEM"].ToString();
            chatConversasModel.FlgManual = String.IsNullOrWhiteSpace(oDataRow["FLG_MANUAL"].ToString()) ? false : true;
            return chatConversasModel;
        }

        */

        #region QUERY

        internal string query_delete_chat_conversa = @"
            DELETE FROM CHAT_CONVERSAS
            WHERE FK_CHAT = @FK_CHAT AND ID_CHAT_CONVERSAS = @IdChatConversas
            AND ID_LOGIN_REMETENTE = @IdLoginRemetente
        ";

        internal string query_update_chat_conversa = @"
            UPDATE CHAT_CONVERSAS SET CONVERSA = @CONVERSA
            WHERE FK_CHAT = @FK_CHAT AND ID_CHAT_CONVERSAS = @IdChatConversas 
        ";

        internal string query_select_Ultima_conversa = @"
            
            SELECT TOP 1 ID_CHAT_CONVERSAS FROM CHAT_CONVERSAS 
            WHERE FK_CHAT = @FK_CHAT AND ID_LOGIN_REMETENTE = @IdLoginRemetente 
            ORDER BY DATA DESC

        ";

        internal string query_select_conversa_remetente = @"
            
            SELECT TOP 1 ID_CHAT_CONVERSAS FROM CHAT_CONVERSAS 
            WHERE FK_CHAT = @FK_CHAT AND ID_LOGIN_REMETENTE = @IdLoginRemetente AND ID_CHAT_CONVERSAS = @IdChatConversas
            ORDER BY DATA DESC

        ";

        internal string query_insert_chat_conversas = @"
            INSERT INTO [dbo].[CHAT_CONVERSAS]     
            ([FK_CHAT]     
            ,[DATA]     
            ,[CONVERSA]     
            ,[ID_LOGIN_REMETENTE]     
            ,[DS_LOGIN_REMETENTE] 
	        ,[ORIGEM])     
            VALUES     
            (@FK_CHAT     
            ,GETDATE()     
            ,@CONVERSA     
            ,@ID_LOGIN_REMETENTE     
            ,@DS_LOGIN_REMETENTE 
	        ,@ORIGEM) 
        ";

        internal string query_select_chat_conversas_listar = @"
             SELECT    
              ID_CHAT_CONVERSAS   
              , FK_CHAT   
              , DATA   
              , CONVERSA   
              , ID_LOGIN_REMETENTE   
              , DS_LOGIN_REMETENTE 
              , ORIGEM    
              , FLG_MANUAL
             FROM    
              CHAT_CONVERSAS WITH (NOLOCK)   
             WHERE    
              FK_CHAT = @FK_CHAT    
             ORDER BY    
              ID_CHAT_CONVERSAS
        ";

        internal string query_select_chat_conversas_listar_por_FkChat_Conversa = @"
             SELECT
                 ID_CHAT_CONVERSAS,
                 FK_CHAT,
                 DATA,
                 CONVERSA,
                 ID_LOGIN_REMETENTE,
                 DS_LOGIN_REMETENTE,
                 ORIGEM,
                 FLG_MANUAL
             FROM
                 CHAT_CONVERSAS WITH (NOLOCK)
             WHERE
                 FK_CHAT = @FK_CHAT
                 AND CONVERSA = @CONVERSA
             ORDER BY
                 ID_CHAT_CONVERSAS
        ";

        #endregion
    }
}
