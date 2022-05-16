using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using Orizon.Rest.Chat.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]

    public class ChatRepository : BaseRepositorio, IChatRepository
    {
        private readonly IChatConversasRepository _chatConversasRepository;
        private readonly IChatLeituraRepository _chatLeituraRepository;

        public ChatRepository(
            PrefatDbContext prefatDbContext
            , DativaDbContext dativaDbContext
            , IChatConversasRepository chatConversasRepository
            , IChatLeituraRepository chatLeituraRepository)
            : base(prefatDbContext, dativaDbContext)
        {
            _chatConversasRepository = chatConversasRepository;
            _chatLeituraRepository = chatLeituraRepository;
        }

        public int Insert(int idLogin)
        {
            BeginTransactionPrefat();
            var sql =
                $@"
                    INSERT INTO
                        [dbo].[CHAT] ([DATA], [ID_LOGIN])
                    VALUES
                        (GETDATE(), {idLogin})
                    SELECT @@IDENTITY
                ";
            var id = _prefatDbContext.Connection.ExecuteScalar(
                    sql: sql,
                    transaction: TransactionPrefat
                );
            CommitPrefat();
            return Convert.ToInt32(id);
        }

        public void Lido(int idChat, int idLogin)
        {
            BeginTransactionPrefat();
            var sql =
                $@"
                  INSERT INTO
                      CHAT_LEITURA (DATA_LEITURA, ID_LOGIN, FK_CHAT_CONVERSAS)
                  SELECT
                      GETDATE(),
                      {idLogin},
                      ID_CHAT_CONVERSAS
                  FROM
                      CHAT_CONVERSAS WITH (NOLOCK)
                  WHERE
                      FK_CHAT = {idChat}
                ";
            _prefatDbContext.Connection.ExecuteScalar(
                    sql: sql,
                    transaction: TransactionPrefat
                );
            CommitPrefat();
        }

        public IEnumerable<ChatE> Listar(int? idChat, int idLogin)
        {
            return ListarImpl(idChat, idLogin, null);
        }

        public IEnumerable<ChatE> Listar(int? idChat, int idLogin, string origem)
        {
            return ListarImpl(idChat, idLogin, origem);
        }

        private IEnumerable<ChatE> ListarImpl(int? idChat, int idLogin, string origem)
        {
            OpenConnectionPrefat();
            var sql =
                $@"
                  SELECT
                      ID_CHAT,
                      DATA,
                      ID_LOGIN
                  FROM
                      CHAT WITH (NOLOCK)
                  WHERE
                      ID_CHAT = {idChat}
                ";
            var chatsModels = _prefatDbContext.Connection.Query<ChatE>(
                    sql: sql,
                    commandType: CommandType.Text
                );
            foreach (var chatModel in chatsModels)
            {
                if (origem == null)
                    chatModel.Conversas = _chatConversasRepository.Listar(chatModel.IdChat);
                else
                    chatModel.Conversas = _chatConversasRepository.Listar(chatModel.IdChat, origem);

                chatModel.QtdeNaoLidas = _chatLeituraRepository.NaoLidas(chatModel.IdChat, idLogin);
            }
            return chatsModels;
        }
    }
}