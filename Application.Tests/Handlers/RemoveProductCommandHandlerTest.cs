using Application.UseCases.RemoverProduto;
using Domain.Interfaces.Repositories;
using Moq;
using System.Net;

namespace Application.Tests.Handlers;

public class RemoveProductCommandHandlerTest
{
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
    private readonly RemoveProductCommandHandler _handler;

    public RemoveProductCommandHandlerTest()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _produtoRepositoryMock = new Mock<IProdutoRepository>();

        _uowMock.SetupGet(u => u.ProdutoRepository).Returns(_produtoRepositoryMock.Object);

        _handler = new RemoveProductCommandHandler(_uowMock.Object);
    }

    [Fact]
    public async Task Handle_Remove_Product_DeveRetornarNotFound_Quando_Produto_Nao_Encontrado()
    {
        var request = new RemoveProductCommand(Guid.NewGuid());

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.NotFound, result.HttpStatusCode);
        Assert.Equal("Produto não encontrado", result.ErrorMessage);
    }
}
