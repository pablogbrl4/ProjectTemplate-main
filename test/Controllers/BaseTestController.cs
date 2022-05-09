using Microsoft.Extensions.Logging;
using Moq;
using Orizon.Rest.Chat.API.Controllers;
using Orizon.Rest.Chat.Application.DTOs;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Application.Requests;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Paginacao;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services
{
    public class BaseTestController<T, TDTO>
        where T : BaseEntidade
        where TDTO : BaseEntidadeDTO
    {
        protected readonly Mock<IBaseApp<T, TDTO>> _moqApp;
        protected readonly ILogger _logger;

        public BaseTestController(ILogger logger)
        {
            _moqApp = new Mock<IBaseApp<T, TDTO>>();
            _logger = logger;
        }

        [Fact]
        public void TesteComMock()
        {
            MockBaseApp();

            // Arrange
            var controller = new CoreController<T, TDTO>(
                    app: _moqApp.Object,
                    logger: _logger
                );

            // Act
            var result = controller.ListarPorPaginacao(
                    new PagesClienteRequest()
                );

            // Assert
            var viewResult = Assert.IsType<PaginacaoModel<TDTO>>(result);
            Assert.NotEqual(0, viewResult.TotalPaginas);
            Assert.NotNull(viewResult.Itens);
        }

        private void MockBaseApp()
        {
            var paginacaoModel = new PaginacaoModel<TDTO>
            {
                PaginaAtual = 1,
                Itens = new List<TDTO>(),
                TotalItens = 100,
                TotalPaginas = 10
            };

            _moqApp.Setup(_ => _.BuscarTodosPaginacao(c => true, 100, 1, default, default, false));
        }
    }
}
