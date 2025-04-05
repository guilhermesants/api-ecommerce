using Application.Common.Responses;
using Application.Dtos;
using MediatR;

namespace Application.UseCases.ObterProdutoPorId;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProdutoDto>>;