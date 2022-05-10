using AutoMapper;
using Moq;
using Orizon.Rest.Chat.Application.DTOs;
using Orizon.Rest.Chat.Application.Services;
using Orizon.Rest.Chat.Domain.Entities;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using Orizon.Rest.Chat.Domain.Paginacao;
using System.Collections.Generic;
using Xunit;

namespace Tests.ServicesApplication
{
    public class BaseTestApplication<T, TDTO>
        where T : BaseEntidade
        where TDTO : BaseEntidadeDTO
    {
        protected readonly Mock<IBaseServico<T>> _moqService;
        protected readonly IMapper _mapper;

        public BaseTestApplication(IMapper mapper)
        {
            _moqService = new Mock<IBaseServico<T>>();
            _mapper = mapper;
        }

        [Fact]
        public void TesteComMock()
        {
            MockBaseServico();

            // Arrange
            var servicoApp = new BaseServicoApp<T, TDTO>(
                    service: _moqService.Object,
                    mapper: _mapper
                );

            // Act
            //var result = servicoApp.BuscarTodosPaginacao(
            //        expression: c => true,
            //        limit: 10,
            //        page: 1,
            //        cancellationToken: default
            //    );

            // Assert
            //var viewResult = Assert.IsType<PaginacaoModel<TDTO>>(result);
            //Assert.NotEqual(0, viewResult.TotalPaginas);
            //Assert.NotNull(viewResult.Itens);
        }

        private void MockBaseServico()
        {
            var paginacaoModel = new PaginacaoModel<TDTO>
            {
                PaginaAtual = 1,
                Itens = new List<TDTO>(),
                TotalItens = 100,
                TotalPaginas = 10
            };

            //_moqService.Setup(_ => _.BuscarTodosPaginacao(c => true, 100, 1, default, default, false));
        }
    }
}
