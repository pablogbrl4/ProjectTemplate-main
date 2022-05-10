using Microsoft.AspNetCore.Mvc;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Application.Requests;
using Orizon.Rest.Chat.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.API.Controllers
{
    //[OrizonLogFilter(
    //    LogHeaders = new string[] { },
    //    LogJwtClaims = new string[] { "idComprador", "idPrestador", "nomeUsuario" }
    //)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatConversasController : Controller
    {
        private HttpRequestMessage Request;

        private readonly IGuiaApp _guiaApp;
        private readonly IChatApp _chatApp;
        private readonly IDadosAuditorApp _dadosAuditorApp;
        private readonly IChatConversasApp _chatConversasApp;
        //private readonly IGeradorEventos _geradorEventos;
        private readonly ICicloAuditoriaApp _cicloAuditoriaDao;
        //private readonly IProxy _proxy;
        private readonly IProxyApp _proxyApp;

        public ChatConversasController(
            IChatConversasApp chatConversasApp
            , IChatApp chatApp
            , IDadosAuditorApp dadosAuditorApp
            , IGuiaApp GuiaDao
            , ICicloAuditoriaApp cicloAuditoriaApp
            //, IGeradorEventos geradorEventos
            //, IProxy proxy
            , IProxyApp proxyApp)
        {
            _guiaApp = GuiaDao;
            _chatApp = chatApp;
            _dadosAuditorApp = dadosAuditorApp;
            _chatConversasApp = chatConversasApp;
            _cicloAuditoriaDao = cicloAuditoriaApp;
            //_geradorEventos = geradorEventos;
            //_proxy = proxy;
            _proxyApp = proxyApp;
        }

        /// <summary>
        /// Retorna as mensagens do Chat informado.
        /// </summary>
        /// <remarks>Rest Chat ID: Chat_Conversas</remarks>
        /// <param name="fkChat">O identificador do Chat.</param>
        /// <returns>Lista de mensagens.</returns>
        [HttpGet]
        [Route("")]
        //[SwaggerResponse(200, "Ok", typeof(ChatConversasModel))]
        //[SwaggerOperation(operationId: "Chat_Conversas")]
        public IActionResult Get(int fkChat)
        {
            Log.Debug(" *********ChatConversasController - Get Iniciado*********");
            Log.Debug("fkChat: " + fkChat);

            try
            {
                //string origem;
                //if (Request.TryGetOrigemRequest(out origem))
                //{
                //    return _chatConversasApp.Listar(fkChat, origem);
                //}

                var list = _chatConversasApp.Listar(fkChat);

                return Ok(list);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);
                throw;
            }
        }

        /// <summary>
        /// Adiciona uma nova mensagem ao Chat informado.
        /// </summary>
        /// <remarks>Rest Chat ID: Chat_Conversas</remarks>
        /// <param name="mensagem">Mensagem a ser adicinada.</param>
        /// <returns>Retorna #0 em caso de sucesso, #-1 caso contrário.</returns>
        //[SwaggerResponse(200, "Ok", typeof(int))]
        //[SwaggerOperation(operationId: "Chat_Conversas")]
        [HttpPost]
        [Route("")]
        public async Task<int> Post(Mensagem mensagem)
        {
            Log.Debug(" *********ChatConversasController - Post Iniciado*********");
            Log.Debug("Mensagem: " + mensagem);

            try
            {
                string origem = null;
                //string origem;
                //if (Request.TryGetOrigemRequest(out origem) && origem.Equals(OrigemRequest.Auditor))
                //{
                //    var dadosAuditor = _dadosAuditorApp.GetDadosAuditorByIdLogin(mensagem.IdLoginRemetente);

                //    mensagem.DsLoginRemetente = dadosAuditor?.Nome ?? mensagem.DsLoginRemetente;
                //}

                if (string.IsNullOrEmpty(origem))
                {
                    Log.Error("Não é posível inserir mensagem sem informar uma origem");
                    return -1;
                }

                Log.Debug("Iniciando Consulta");
                var chatconversas = _chatConversasApp.ListarPorFkChatConversa(mensagem.FkChat, mensagem.Conversa);
                if (chatconversas != null && chatconversas.Any())
                {
                    var chatconversa = chatconversas.FirstOrDefault();
                    if (chatconversa.Data != null)
                    {
                        DateTime data = (DateTime)chatconversa.Data;
                        if (DateTime.Now >= data && DateTime.Now <= data.AddSeconds(2))
                        {
                            Log.Error("Não é possível inserir mensagens repetidas");
                            return 0;
                        }
                    }
                }

                Log.Debug("Iniciando Insert");
                _chatConversasApp.Insert(mensagem, origem);

                Log.Debug("Iniciando Lido");
                _chatApp.Lido(mensagem.FkChat, mensagem.IdLoginRemetente);

                await AlterarStatusApontamento(mensagem, origem);

                RegistrarUltimoLoginAlteracao(mensagem, origem);

                return 0;
            }
            catch (Exception error)
            {
                Log.Error(error.Message);
                return -1;
            }
        }

        /// <summary>
        /// Atualiza a mensagem do Chat informado.
        /// </summary>
        /// <remarks>Rest Chat ID: Chat_Conversas</remarks>
        /// <param name="mensagem">Mensagem a ser adicinada.</param>
        /// <returns>Retorna #0 em caso de sucesso, #-1 caso contrário.</returns>
        //[SwaggerResponse(200, "Ok", typeof(int))]
        //[SwaggerOperation(operationId: "Chat_Conversas")]
        [HttpPut]
        [Route("")]
        public int Put(Mensagem mensagem)
        {
            Log.Debug(" *********ChatConversasController - Put Iniciado*********");

            try
            {
                //string origem;
                //if (Request.TryGetOrigemRequest(out origem) && origem.Equals(OrigemRequest.Auditor))
                //{
                //    var dadosAuditor = _dadosAuditorApp.GetDadosAuditorByIdLogin(mensagem.IdLoginRemetente);

                //    mensagem.DsLoginRemetente = dadosAuditor?.Nome ?? mensagem.DsLoginRemetente;
                //}

                //if (string.IsNullOrEmpty(origem))
                //{
                //    Log.Error("Não é posível inserir mensagem sem informar uma origem");
                //    return -1;
                //}

                Log.Debug("Iniciando Atualizacao Chat");

                var idChatConversa = _chatConversasApp.BuscarUltimaMensagemChat(mensagem.FkChat, mensagem.IdLoginRemetente);

                if (idChatConversa == 0)
                    return -1;

                _chatConversasApp.AtualizarMensagemChat(mensagem.FkChat, idChatConversa, mensagem.Conversa);

                return 0;
            }
            catch (Exception error)
            {
                Log.Error(error.Message);
                return -1;
            }
        }

        /// <summary>
        /// Delete a mensagem do Chat informado.
        /// </summary>
        /// <remarks>Rest Chat ID: Chat_Conversas</remarks>
        /// <param name="mensagem">Mensagem a ser adicinada.</param>
        /// <returns>Retorna #0 em caso de sucesso, #-1 caso contrário.</returns>
        //[SwaggerResponse(200, "Ok", typeof(int))]
        //[SwaggerOperation(operationId: "Chat_Conversas")]
        [HttpDelete]
        [Route("")]
        public IActionResult Delete(Mensagem mensagem)
        {
            Log.Debug(" *********ChatConversasController - Put Iniciado*********");

            try
            {
                //string origem;
                //if (Request.TryGetOrigemRequest(out origem) && origem.Equals(OrigemRequest.Auditor))
                //{
                //    var dadosAuditor = _dadosAuditorApp.GetDadosAuditorByIdLogin(mensagem.IdLoginRemetente);

                //    mensagem.DsLoginRemetente = dadosAuditor?.Nome ?? mensagem.DsLoginRemetente;
                //}

                //if (string.IsNullOrEmpty(origem))
                //{
                //    return BadRequest("Não é posível inserir mensagem sem informar uma origem");
                //}

                var possuiConversa = _chatConversasApp.BuscarChatRemetente(mensagem.IdChatConversas, mensagem.IdLoginRemetente, mensagem.FkChat);

                if (!possuiConversa)
                    return BadRequest("Não foram encontradas mensagens para este remente!");

                _chatConversasApp.DeletarChatConversas(mensagem.FkChat, mensagem.IdChatConversas, mensagem.IdLoginRemetente);

                return Ok();
            }
            catch (Exception error)
            {
                Log.Error(error.Message);
                return BadRequest(error);
            }
        }

        #region MetodosPrivados

        private async Task AlterarStatusApontamento(Mensagem mensagem, string origem)
        {
            if (mensagem.Tipo == MensagemChat.TipoApontamentoItem)
            {
                await AlterarStatusApontamentoItem(mensagem, origem);
            }
            else if (mensagem.Tipo == MensagemChat.TipoApontamentoGuia)
            {
                await AlterarStatusApontamentoGuiaNegociacao(mensagem, origem);
            }
        }

        private async Task AlterarStatusApontamentoGuiaNegociacao(Mensagem mensagem, string origem)
        {
            if (origem == MensagemChat.OrigemPrestador)
            {
                //TODO
                // var sUrl = _proxy.RetornaEnderecoInterno("preFatStatusContaNegociacao");

                var sUrl = "";
                sUrl = sUrl + $"?idConta={mensagem.Id}&observacao={mensagem.Conversa}&idStatus=";

                switch (origem)
                {
                    case MensagemChat.OrigemPrestador:
                        switch (mensagem.Acao)
                        {
                            case (int)Acao.Justificar:
                                sUrl = sUrl + (int)StatusContaNegociacao.Justificada;

                                break;
                            case (int)Acao.Aceitar:
                                sUrl = sUrl + (int)StatusContaNegociacao.Aceita;

                                break;
                        }
                        break;
                }

                Log.Debug("Iniciando chamada para alterar o status guia negociacao...");
                Log.Debug("sUrl: " + sUrl);
                var retorno = await _proxyApp.PostAsync(sUrl, Request);
                Log.Debug($"Alterando status guia negociacao {retorno}");
            }
        }

        private async Task AlterarStatusApontamentoItem(Mensagem mensagem, string origem)
        {
            if ((origem == MensagemChat.OrigemPrestador) || (origem == MensagemChat.OrigemAuditor))
            {
                //TODO
                //var sUrl = _proxy.RetornaEnderecoInterno("preFatStatusItem");
                var sUrl = "";
                sUrl = sUrl + $"?Id_Item={mensagem.Id}&Status=";

                var cicloDados = _cicloAuditoriaDao.GetCicloDadosApontamento(Convert.ToInt32(mensagem.Id));

                switch (origem)
                {
                    case MensagemChat.OrigemPrestador:
                        switch (mensagem.Acao)
                        {
                            case (int)Acao.Justificar:
                                {
                                    sUrl = sUrl + (int)StatusItem.Justificado;
                                    //_geradorEventos.ItemJustificado(cicloDados.protocolo, mensagem.GuiaId, Convert.ToInt32(mensagem.Id), cicloDados.descricaoItem, cicloDados.ciclo, cicloDados.quantidade ?? 0, cicloDados.valorUnidade ?? 0, cicloDados.observacao);
                                    break;
                                }

                            case (int)Acao.Aceitar:
                                {
                                    sUrl = sUrl + (int)StatusItem.Aceito;
                                    //_geradorEventos.ItemAceito(cicloDados.protocolo, mensagem.GuiaId, Convert.ToInt32(mensagem.Id), cicloDados.descricaoItem, cicloDados.ciclo, cicloDados.quantidade ?? 0, cicloDados.valorUnidade ?? 0, cicloDados.observacao);
                                    break;
                                }

                        }
                        break;
                }

                Log.Debug("Iniciando chamada para alterar o status do item...");
                Log.Debug("sUrl: " + sUrl);
                var retorno = await _proxyApp.PostAsync(sUrl, Request);
                Log.Debug($"Alterando status do item {retorno}");
            }
        }

        private void RegistrarUltimoLoginAlteracao(Mensagem mensagem, string origem)
        {
            //Registrar ultimo Login do Prestador/Auditor
            if (mensagem.GuiaId > 0)
            {
                //TODO
                // var ultimoLogin = Token.Token.RetornaToken(Request).nomeUsuario;
                var ultimoLogin = "";
                switch (origem)
                {
                    case MensagemChat.OrigemPrestador:
                        _guiaApp.AtribuirNomePrestadorUltimaModificacao(new Guia { ID_CONTA = mensagem.GuiaId, ULTIMO_LOGIN = ultimoLogin });
                        break;
                    case MensagemChat.OrigemAuditor:
                        _guiaApp.AtribuirNomeAuditorUltimaModificacao(new Guia { ID_CONTA = mensagem.GuiaId, ULTIMO_LOGIN = ultimoLogin });
                        break;
                }
            }
        }

        #endregion MetodosPrivados
    }
}
