using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Enums;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]

    public class ChatConversasRepository : BaseRepositorio, IChatConversasRepository
    {
        private static readonly string AuditorTitulo = "Analista";

        public ChatConversasRepository(PrefatDbContext prefatDbContext, DativaDbContext dativaDbContext)
            : base(prefatDbContext, dativaDbContext)
        {
        }

        /// <summary>
        /// Inserir Chat Mensagem
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="origem"></param>
        public void Insert(Mensagem mensagem, string origem)
        {
            BeginTransactionPrefat();
            var sql =
                $@"
                 INSERT INTO
                     [dbo].[CHAT_CONVERSAS] (
                         [FK_CHAT],
                         [DATA],
                         [CONVERSA],
                         [ID_LOGIN_REMETENTE],
                         [DS_LOGIN_REMETENTE],
                         [ORIGEM]
                     )
                 VALUES
                     (
                         {mensagem.FkChat},
                         GETDATE(),
                         {mensagem.Conversa},
                         {mensagem.IdLoginRemetente},
                         {mensagem.DsLoginRemetente},
                         {origem}
                     )
                ";
            _prefatDbContext.Connection.Execute(
                    sql: sql,
                    transaction: TransactionPrefat
                );
            CommitPrefat();
        }

        /// <summary>
        /// Atualizar Chat Mensagem
        /// </summary>
        /// <param name="IdChatConversas"></param>
        /// <param name="fkchat"></param>
        /// <param name="conversa"></param>
        public void AtualizarMensagemChat(int fkchat, int IdChatConversas, string conversa)
        {
            BeginTransactionPrefat();
            var sql =
                $@"
                  UPDATE
                      CHAT_CONVERSAS
                  SET
                      CONVERSA = {conversa}
                  WHERE
                      FK_CHAT = {fkchat}
                      AND ID_CHAT_CONVERSAS = {IdChatConversas}
                ";
            _prefatDbContext.Connection.Execute(
                    sql: sql,
                    transaction: TransactionPrefat
                );
            CommitPrefat();
        }

        /// <summary>
        /// Buscar ultima mensagem
        /// </summary>
        /// <param name="remetente"></param>
        /// <param name="FkChat"></param>
        public int BuscarUltimaMensagemChat(int FkChat, int remetente)
        {
            OpenConnectionPrefat();
            var sql = $@"
                        SELECT
                            TOP 1 ID_CHAT_CONVERSAS
                        FROM
                            CHAT_CONVERSAS
                        WHERE
                            FK_CHAT = {FkChat}
                            AND ID_LOGIN_REMETENTE = {remetente}
                        ORDER BY
                            DATA DESC
                      ";
            return _prefatDbContext.Connection.ExecuteScalar<int>(
                    sql: sql,
                    commandType: CommandType.Text
                );
        }

        public bool BuscarChatRemetente(int IdChatConversas, int remetente, int fkChat)
        {
            OpenConnectionPrefat();
            var sql =
                $@"
                  SELECT
                      TOP 1 ID_CHAT_CONVERSAS
                  FROM
                      CHAT_CONVERSAS
                  WHERE
                      FK_CHAT = {fkChat}
                      AND ID_LOGIN_REMETENTE = {remetente}
                      AND ID_CHAT_CONVERSAS = {IdChatConversas}
                  ORDER BY
                      DATA DESC
                ";
            var idChatConversa = _prefatDbContext.Connection.ExecuteScalar<int>(
                    sql: sql,
                    commandType: CommandType.Text
                );

            return idChatConversa != 0;
        }

        /// <summary>
        /// Atualizar Chat Mensagem
        /// </summary>
        /// <param name="IdChatConversas"></param>
        /// <param name="fkchat"></param>
        /// <param name="idRemetente"></param>
        public void DeletarChatConversas(int fkchat, int IdChatConversas, int idRemetente)
        {
            BeginTransactionPrefat();
            var sql =
                $@"
                  DELETE FROM
                      CHAT_CONVERSAS
                  WHERE
                      FK_CHAT = {fkchat}
                      AND ID_CHAT_CONVERSAS = {IdChatConversas}
                      AND ID_LOGIN_REMETENTE = {idRemetente}
                ";
            _prefatDbContext.Connection.Execute(
                    sql: sql,
                    transaction: TransactionPrefat
                );
            CommitPrefat();
        }

        /// <summary>
        /// Listar Chat Mensagem
        /// </summary>
        /// <param name="fkChat"></param>
        /// <returns></returns>
        public IEnumerable<ChatConversas> Listar(int fkChat)
        {
            return ListarImpl(fkChat, null);
        }

        public IEnumerable<ChatConversas> Listar(int fkChat, string origem)
        {
            return ListarImpl(fkChat, origem.Equals(Origem.Prestador) ? AuditorTitulo : null);
        }

        public IEnumerable<ChatConversas> ListarPorFkChatConversa(int fkChat, string conversa)
        {
            OpenConnectionPrefat();
            var sql = $@"
                        SELECT
                            ID_CHAT_CONVERSAS AS {nameof(ChatConversas.IdChatConversas)},
                            FK_CHAT AS {nameof(ChatConversas.FkChat)},
                            DATA AS {nameof(ChatConversas.Data)},
                            CONVERSA AS {nameof(ChatConversas.Conversa)},
                            ID_LOGIN_REMETENTE AS {nameof(ChatConversas.IdLoginRemetente)},
                            DS_LOGIN_REMETENTE AS {nameof(ChatConversas.DsLoginRemetente)},
                            ORIGEM AS {nameof(ChatConversas.Origem)},
                            FLG_MANUAL AS {nameof(ChatConversas.FlgManual)}
                        FROM
                            CHAT_CONVERSAS WITH (NOLOCK)
                        WHERE
                            FK_CHAT = {fkChat}
                            AND CONVERSA = '{conversa}'
                        ORDER BY
                            ID_CHAT_CONVERSAS
                      ";
            var chatsConversas = _prefatDbContext.Connection.Query<ChatConversas>(
                    sql: sql,
                    commandType: CommandType.Text
                );
            foreach (var item in chatsConversas)
            {
                if (item.FlgManual == null)
                    item.FlgManual = false;
            }
            return chatsConversas;
        }

        private IEnumerable<ChatConversas> ListarImpl(int fkChat, string remetente)
        {
            OpenConnectionPrefat();
            var sql = $@"
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
                            FK_CHAT = {fkChat}
                        ORDER BY
                            ID_CHAT_CONVERSAS
                      ";
            var chatsConversas = _prefatDbContext.Connection.Query<ChatConversas>(
                    sql: sql,
                    commandType: CommandType.Text
                );
            foreach (var obj in chatsConversas)
            {
                if (obj.FlgManual == null)
                    obj.FlgManual = false;

                if (obj.Origem == Origem.Auditor)
                    obj.DsLoginRemetente = remetente ?? obj.DsLoginRemetente;
            }
            return chatsConversas;
        }
    }
}
