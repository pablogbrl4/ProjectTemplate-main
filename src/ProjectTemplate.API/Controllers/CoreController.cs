using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectTemplate.Application.DTOs;
using ProjectTemplate.Application.Interfaces;
using ProjectTemplate.Application.Requests;
using ProjectTemplate.Domain.Entities;
using ProjectTemplate.Domain.Paginacao;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectTemplate.API.Controllers
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
                var list = await _app.BuscarTodosPaginacao(c => true, urlQueryParameters.Limit, urlQueryParameters.Page, cancellationToken);

                if (list.PaginaAtual > 1)
                {
                    var prevRoute = $"/api/clientes?limit={urlQueryParameters.Limit}&page={urlQueryParameters.Page - 1}";
                    list.AddResourceLink(LinkedResourceType.Prev, prevRoute);
                }

                if (list.PaginaAtual < list.TotalPaginas)
                {
                    var nextRoute = $"/api/clientes?limit={urlQueryParameters.Limit}&page={urlQueryParameters.Page + 1}";
                    list.AddResourceLink(LinkedResourceType.Next, nextRoute);
                }

                return new OkObjectResult(list);
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
                var objById = await _app.BuscarPorId(id);
                return new OkObjectResult(objById);
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
                await _app.IniciarTransaction();
                var id = await _app.Incluir(dado);
                await _app.SalvarMudancas();
                return new OkObjectResult(id);
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
                await _app.IniciarTransaction();
                await _app.IncluirLista(dados);
                await _app.SalvarMudancas();
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
                await _app.IniciarTransaction();
                _app.Alterar(dado);
                await _app.SalvarMudancas();
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
                await _app.IniciarTransaction();
                _app.AlterarLista(dados);
                await _app.SalvarMudancas();
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
                var objEntity = await _app.BuscarPorId(id);

                if (objEntity == null)
                    return NotFound();

                patchEntity.ApplyTo(objEntity, ModelState);

                await _app.IniciarTransaction();
                _app.Alterar(objEntity);
                await _app.SalvarMudancas();

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
                await _app.IniciarTransaction();
                await _app.Excluir(id);
                await _app.SalvarMudancas();
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
