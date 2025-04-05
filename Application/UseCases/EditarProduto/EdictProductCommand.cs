using Application.Common.Responses;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.EditarProduto;

public record EdictProductCommand
(
    Guid Id,
    ProdutoDto ProdutoDto
) : IRequest<Result<Unit>>;

