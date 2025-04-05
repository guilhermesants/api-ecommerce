using Application.Common.Responses;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ObterProdutos;

public record GetProductsQuery(
    string? Categoria,
    decimal? PrecoMinimo,
    decimal? PrecoMaximo,
    bool? Ativo,
    int? Pagina,
    int? QtdPagina
) : IRequest<Result<GetProductsQueryResponse>>;

public record GetProductsQueryResponse(
    IEnumerable<ProdutoDto> ListProducts,
    int Pagina,
    int QtdPagina,
    int TotalItens
);