using Application.Common.Responses;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.ObterProdutos;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProdutoDto>>>
{
    private readonly IUnitOfWork _uow;

    public GetProductsQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<IEnumerable<ProdutoDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var produtos = await _uow.ProdutoRepository.ObterProdutosPorFiltroAsync(
            request.Categoria,
            request.PrecoMinimo,
            request.PrecoMaximo,
            request.Ativo,
            cancellationToken
        );

        if (!produtos.Any())
            return Result<IEnumerable<ProdutoDto>>.FailureWithStatusCode("Produtos não encontrados", HttpStatusCode.NotFound);

        var produtosDto = produtos.Select(p => new ProdutoDto
        (
            p.Nome,
            p.Valor,
            p.QtdEstoque,
            p.Categoria.Nome,
            p.UrlImagem,
            p.Ativo
        )).ToList();

        return Result<IEnumerable<ProdutoDto>>.SuccessWithStatusCode(produtosDto, HttpStatusCode.OK);
    }
}
