using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NlogOrizon.Filters;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.API.Controllers
{
    [OrizonLogFilter(
        LogHeaders = new string[] { },
        LogJwtClaims = new string[] { "idComprador", "idPrestador", "nomeUsuario" }
    )]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApontamentoController : Controller
    {
        private readonly IApontamentoApp _apontamentoApp;

        public ApontamentoController(IApontamentoApp apontamentoApp)
        {
            _apontamentoApp = apontamentoApp;
        }

        /// <summary>
        /// Insere a mensagem de multiplos Chats.
        /// </summary>
        /// <remarks>Rest Link ID: chat_apontamento_questionar_item</remarks>
        /// <param name="content">Lista de mensagens</param>
        /// <returns>"Sucesso", se os dados forem inseridos, ou "Lista Vaazia".</returns>
        /// 
        [HttpPost]
        [Route("")]
        [SwaggerResponse(200, "Ok", typeof(string))]
        [SwaggerOperation(summary: "chat_apontamento_questionar_item")]
        public async Task<IActionResult> Post([FromBody] string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var mensagens = JsonConvert.DeserializeObject<Mensagem[]>(content);

                if (mensagens.Length > 0)
                {
                    _apontamentoApp.InserirDados(mensagens);
                    return await Task.FromResult(Ok("Sucesso"));
                }
            }
            return await Task.FromResult(Ok("Lista Vazia"));
        }
    }
}
