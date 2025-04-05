using Application.Common.Responses;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.ObterProdutos;

public class GetProductsQueryHandler : HandlerBase, IRequestHandler<GetProductsQuery, Result<GetProductsQueryResponse>>
{
    public GetProductsQueryHandler(IUnitOfWork uow) : base(uow) { }

    public async Task<Result<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var pagina = request.Pagina ?? 1;
        var qtdPagina = request.QtdPagina ?? 10;

        var (produtos, totalItens) = await _uow.ProdutoRepository.ObterProdutosPorFiltroAsync(
            request.Categoria,
            request.PrecoMinimo,
            request.PrecoMaximo,
            request.Ativo,
            pagina,
            qtdPagina,
            cancellationToken
        );

        if (!produtos.Any())
            return Result<GetProductsQueryResponse>.FailureWithStatusCode("Produtos não encontrados", HttpStatusCode.NotFound);

        var produtosDto = produtos.Select(p => new ProdutoDto
        (
            p.Nome,
            p.Valor,
            p.QtdEstoque,
            p.Categoria.Nome,
            p.UrlImagem,
            p.Ativo
        )).ToList();

        return Result<GetProductsQueryResponse>.SuccessWithStatusCode(
            new GetProductsQueryResponse(
                 produtosDto,
                 pagina,
                 qtdPagina,
                 totalItens
            ), HttpStatusCode.OK);
    }
}
