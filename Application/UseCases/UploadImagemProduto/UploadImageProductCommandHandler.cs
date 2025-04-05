using Application.Common.Responses;
using Application.Services.Abstracts;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.UploadImagemProduto;

public class UploadImageProductCommandHandler :HandlerBase, IRequestHandler<UploadImageProductCommand, Result<string>>
{
    private readonly IStorageService _storageService;

    public UploadImageProductCommandHandler(IUnitOfWork uow, IStorageService storageService) : base(uow)
    {
        _storageService = storageService;
    }

public async Task<Result<string>> Handle(UploadImageProductCommand request, CancellationToken cancellationToken)
    {
        var produto = await _uow.ProdutoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (produto is null)
            return Result<string>.FailureWithStatusCode("Produto não encontrado", HttpStatusCode.NotFound);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
        var filePath = await _storageService.UploadFileAsync(request.File, fileName, cancellationToken);

        produto.UrlImagem = filePath;
        await _uow.CommitAsync(cancellationToken);

        return Result<string>.SuccessWithStatusCode(filePath, HttpStatusCode.OK);
    }
}
