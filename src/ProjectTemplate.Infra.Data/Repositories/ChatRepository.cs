using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Orizon.Rest.Chat.Utilities;

namespace Orizon.Rest.Chat.Infra.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        public int Insert(int idLogin)
        {
            return 0;

            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = "PROC_INSERE_CHAT",
                TipoComando = CommandType.StoredProcedure
            };
            bd.CriarParametro("ID_LOGIN", DbType.Int32, idLogin);
            return Convert.ToInt32(bd.ExecutarEscalar());
            */
        }

        public void Lido(int idChat, int idLogin)
        {
            return;

            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = "PROC_CHAT_LEITURA",
                TipoComando = CommandType.StoredProcedure
            };
            bd.CriarParametro("ID_CHAT", DbType.Int32, idChat);
            bd.CriarParametro("ID_LOGIN", DbType.Int32, idLogin);
            bd.ExecutarComando();

            */
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
            return null;

            /*
            var bd = new BancoDados("PreFaturamento")
            {
                TextoComando = "PRC_CHAT_LISTAR",
                TipoComando = CommandType.StoredProcedure
            };

            bd.CriarParametro("ID_CHAT", DbType.Int32, idChat);

            var dataTable = bd.ExecutarDataTable();
            return (from DataRow linha in dataTable.Rows select Bind(linha, idLogin, origem)).ToList();

            */
        }

        private ChatE Bind(DataRow oDataRow, int idLogin, string origem)
        {
            var chatModel = new ChatE
            {
                IdChat = Convert.ToInt32(oDataRow["ID_CHAT"]),
                Data = Converter.ConverterToDateTime(oDataRow["DATA"].ToString()),
                IdLogin = Convert.ToInt32(oDataRow["ID_LOGIN"])
            };

            /*
            if (origem == null)
                chatModel.Conversas = _chatConversasDao.Listar(chatModel.IdChat);
            else
                chatModel.Conversas = _chatConversasDao.Listar(chatModel.IdChat, origem);

            chatModel.QtdeNaoLidas = _chatLeituraDao.NaoLidas(chatModel.IdChat, idLogin);
            */

            return chatModel;
        }
    }
}