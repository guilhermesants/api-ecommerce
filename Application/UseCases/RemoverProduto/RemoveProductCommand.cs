using Application.Common.Responses;
using MediatR;

namespace Application.UseCases.RemoverProduto;

public record RemoveProductCommand
(
    Guid Id
) : IRequest<Result<Unit>>;
