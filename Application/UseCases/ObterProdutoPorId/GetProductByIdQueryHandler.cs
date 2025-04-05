using Application.Common.Responses;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.ObterProdutoPorId;

public class GetProductByIdQueryHandler : HandlerBase, IRequestHandler<GetProductByIdQuery, Result<ProdutoDto>>
{
    public GetProductByIdQueryHandler(IUnitOfWork uow) : base(uow){ }

    public async Task<Result<ProdutoDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var produto = await _uow.ProdutoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (produto is null)
            return Result<ProdutoDto>.FailureWithStatusCode("Produto não encontrado", HttpStatusCode.NotFound);

        return Result<ProdutoDto>.SuccessWithStatusCode(
                new ProdutoDto(
                     produto.Nome,
                     produto.Valor,
                     produto.QtdEstoque,
                     produto.Categoria.Nome,
                     produto.UrlImagem,
                     produto.Ativo
                ), HttpStatusCode.OK);
    }
}
