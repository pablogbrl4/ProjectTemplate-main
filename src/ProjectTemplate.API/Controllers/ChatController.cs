using Microsoft.AspNetCore.Mvc;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Application.Requests;
using Orizon.Rest.Chat.Domain.Entities;
using Serilog;
using System;
using System.Linq;
using System.Net.Http;

namespace Orizon.Rest.Chat.API.Controllers
{
    //[OrizonLogFilter(
    //    LogHeaders = new string[] { },
    //    LogJwtClaims = new string[] { "idComprador", "idPrestador", "nomeUsuario" }
    //)]
    public class ChatController : Controller
    {
        private HttpRequestMessage Request;

        private readonly IChatApp _chatApp;

        public ChatController(IChatApp chatApp)
        {
            _chatApp = chatApp;
        }

        /// <summary>
        /// Retorna um Chat.
        /// </summary>
        /// <remarks>Rest Link ID: Chat_Chat</remarks>
        /// <param name="idChat">O identificador do Chat.</param>
        /// <param name="idLogin">O identificador do login requisitante.</param>
        /// <returns>O chat requisitado.</returns>
        [HttpGet]
        //[SwaggerResponse(200, "Ok", typeof(ChatModel))]
        //[SwaggerOperation(operationId: "Chat_Chat")]
        public ChatE Get(int? idChat, int idLogin)
        {
            Log.Debug($"********** Orizon.Rest.Chat - ChatController: VERBO GET / idChat: {idChat} / idLogin: {idLogin} ************");

            try
            {
                string origem;
                if (Request.TryGetOrigemRequest(out origem))
                {
                    return _chatApp.Listar(idChat, idLogin, origem).ToList()[0];
                }

                return _chatApp.Listar(idChat, idLogin).ToList()[0];
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return new ChatE();
            }
        }

        /// <summary>
        /// Cria um novo Chat.
        /// </summary>
        /// <remarks>Rest Link ID: Chat_Chat</remarks>
        /// <param name="idLogin">O identificador do login requisitante.</param>
        /// <returns>O identificador do Chat criado.</returns>
        [HttpPost]
        //[SwaggerResponse(200, "Ok", typeof(int))]
        //[SwaggerOperation(operationId: "Chat_Chat")]
        public int Post(int? idLogin)
        {
            Log.Debug($"********** Orizon.Rest.Chat - ChatController: VERBO POST / idLogin: {idLogin} ************");
            try
            {
                return _chatApp.Insert((int)idLogin);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return -1;
            }
        }

        /// <summary>
        /// Marca um Chat como lido.
        /// </summary>
        /// <remarks>Rest Link ID: Chat_Chat</remarks>
        /// <param name="idChat">O identificador do Chat.</param>
        /// <param name="idLogin">O identificador do login requisitante.</param>
        [HttpPut]
        //[SwaggerResponse(200, "Ok", typeof(void))]
        //[SwaggerOperation(operationId: "Chat_Chat")]
        public void Put(int idChat, int idLogin)
        {
            Log.Debug($"********** Orizon.Rest.Chat - ChatController: VERBO Put / idChat: {idChat} / idLogin: {idLogin} ************");

            try
            {
                _chatApp.Lido(idChat, idLogin);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }
    }
}
