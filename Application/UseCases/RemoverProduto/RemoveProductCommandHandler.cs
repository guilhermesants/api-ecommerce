using Application.Common.Responses;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.RemoverProduto;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Result<Unit>>
{
    private readonly IUnitOfWork _uow;

    public RemoveProductCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<Unit>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var produto = await _uow.ProdutoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (produto is null)
            return Result<Unit>.FailureWithStatusCode("Produto não encontrado", HttpStatusCode.NotFound);

        _uow.ProdutoRepository.Remove(produto);
        await _uow.CommitAsync(cancellationToken);

        return Result<Unit>.SuccessWithStatusCode(HttpStatusCode.NoContent);
    }
}
