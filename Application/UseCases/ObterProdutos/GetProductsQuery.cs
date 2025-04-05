using Application.Common.Responses;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ObterProdutos;

public record GetProductsQuery(
    string? Categoria,
    decimal? PrecoMinimo,
    decimal? PrecoMaximo,
    bool? Ativo
) : IRequest<Result<IEnumerable<ProdutoDto>>>;