using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orizon.Rest.Chat.Application.DTOs;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Application.Requests;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoreController<T, TDTO> : Controller
        where T : BaseEntidade
        where TDTO : BaseEntidadeDTO
    {
        protected readonly IBaseApp<T, TDTO> _app;
        protected readonly ILogger _logger;

        public CoreController(
            IBaseApp<T, TDTO> app,
            ILogger logger
            )
        {
            _app = app;
            _logger = logger;
        }

        #region Leitura

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarPorPaginacao([FromQuery] PagesClienteRequest urlQueryParameters, CancellationToken cancellationToken = default)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> SelecionarPorId(object id)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Leitura

        #region Escrita

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Incluir([FromBody] TDTO dado)
        {
            try
            {
                return new OkObjectResult(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> IncluirLista([FromBody] List<TDTO> dados)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Alterar(object id, [FromBody] TDTO dado)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> AlterarLista([FromBody] List<TDTO> dados)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(object id, [FromBody] JsonPatchDocument<TDTO> patchEntity)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir(object id)
        {
            try
            {
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Escrita
    }
}
