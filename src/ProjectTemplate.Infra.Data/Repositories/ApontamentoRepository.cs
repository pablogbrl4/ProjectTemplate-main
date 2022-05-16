using Dapper;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]

    public class ApontamentoRepository : BaseRepositorio, IApontamentoRepository
    {
        private readonly IDadosAuditorRepository _dadosAuditorRepository;

        public ApontamentoRepository(
            PrefatDbContext prefatDbContext
            , DativaDbContext dativaDbContext
            , IDadosAuditorRepository dadosAuditorRepository)
                : base(prefatDbContext, dativaDbContext)
        {
            _dadosAuditorRepository = dadosAuditorRepository;
        }

        public void InserirDados(Mensagem[] mensagens)
        {
            var dadosAuditor = _dadosAuditorRepository.GetDadosAuditorByIdLogin(mensagens[0].IdLoginRemetente);
            BeginTransactionPrefat();
            foreach (var msg in mensagens)
            {
                msg.DsLoginRemetente = dadosAuditor?.Nome ?? msg.DsLoginRemetente;
                int idChat = InserirDadosConversa(msg);
                AtualizaIdChat(msg, idChat);
            }
            CommitPrefat();
        }

        private int InserirDadosConversa(Mensagem msg)
        {
            var idChat = _prefatDbContext.Connection.ExecuteScalar(
                sql: "PRC_CHAT_INSERIR_DADOS_APONTAMENTO",
                new
                {
                    ID_LOGIN = msg.IdLoginRemetente,
                    CONVERSA = msg.Conversa,
                    DS_LOGIN = msg.DsLoginRemetente,
                    ID_CHAT = msg.FkChat,
                },
                commandType: CommandType.StoredProcedure,
                transaction: TransactionPrefat);
            return Convert.ToInt32(idChat);
        }

        private void AtualizaIdChat(Mensagem msg, int idChat)
        {
            _prefatDbContext.Connection.ExecuteScalar(
                sql: "PROC_UPDATE_ITEM_CHAT",
                new
                {
                    ID_ITEM = msg.Id,
                    ID_CHAT = idChat
                },
                commandType: CommandType.StoredProcedure,
                transaction: TransactionPrefat);
        }
    }
}
