using Application.Common.Responses;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.AdicionarProduto;

public record NewProductCommand
(
    ProdutoDto ProdutoDto
) : IRequest<Result<NewProductCommandResponse>>;

public record NewProductCommandResponse(Guid Id);
