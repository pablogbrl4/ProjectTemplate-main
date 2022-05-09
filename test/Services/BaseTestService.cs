using Moq;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Paginacao;
using Orizon.Rest.Chat.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services
{
    public class BaseTestService<T> where T : BaseEntidade
    {
        protected readonly Mock<IBaseRepositorio<T>> _moqRepositorio;

        public BaseTestService()
        {
            _moqRepositorio = new Mock<IBaseRepositorio<T>>();
        }

        [Fact]
        public void TesteComMock()
        {
            MockBaseServico();

            // Arrange
            var servico = new BaseServico<T>(
                        repositorio: _moqRepositorio.Object
                    );

            // Act
            var result = servico.BuscarTodosPaginacao(
                    expression: c => true,
                    limit: 10,
                    page: 1,
                    cancellationToken: default
                );

            // Assert
            var viewResult = Assert.IsType<PaginacaoModel<T>>(result);
            Assert.NotEqual(0, viewResult.TotalPaginas);
            Assert.NotNull(viewResult.Itens);
        }

        private void MockBaseServico()
        {
            var paginacaoModel = new PaginacaoModel<T>
            {
                PaginaAtual = 1,
                Itens = new List<T>(),
                TotalItens = 100,
                TotalPaginas = 10
            };

            _moqRepositorio.Setup(_ => _.BuscarTodosPaginacao(c => true, 100, 1, default, default, false));
        }
    }
}
