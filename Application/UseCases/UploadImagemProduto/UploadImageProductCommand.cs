using Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.UploadImagemProduto;

public record UploadImageProductCommand
(Guid Id, IFormFile File) : IRequest<Result<string>>;
