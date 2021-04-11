using Microsoft.AspNetCore.Mvc;
using ProjectTemplate.Application.DTOs;
using ProjectTemplate.Application.Interfaces;
using ProjectTemplate.Domain.Entities;
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

        public CoreController(IBaseApp<T, TDTO> app)
        {
            _app = app;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var list = await _app.BuscarTodos();
                return new OkObjectResult(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("paginacao")]
        public async Task<IActionResult> ListarPorPaginacao(int limit, int page, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _app.BuscarTodosPaginacao(limit, page, cancellationToken);
                return new OkObjectResult(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> SelecionarPorId(int id)
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

        [HttpPost]
        public async Task<IActionResult> Incluir([FromBody] TDTO dado)
        {
            try
            {
                return new OkObjectResult(await _app.Incluir(dado));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> IncluirLista([FromBody] List<TDTO> dados)
        {
            try
            {
                return new OkObjectResult(await _app.IncluirLista(dados));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody] TDTO dado)
        {
            try
            {
                await _app.Alterar(dado);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _app.Excluir(id);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
