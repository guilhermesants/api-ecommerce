using Application.Common.Responses;
using MediatR;

namespace Application.UseCases.AdicionarProduto;

public record NewProductCommand
(
    string Nome,
    decimal Valor,
    int QtdEstoque,
    string NomeCategoria,
    string? UrlImagem
) : IRequest<Result<NewProductCommandResponse>>;

public record NewProductCommandResponse(Guid Id);
