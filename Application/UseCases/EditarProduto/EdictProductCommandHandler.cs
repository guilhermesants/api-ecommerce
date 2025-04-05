using Application.Common.Responses;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.EditarProduto;

public class EdictProductCommandHandler : IRequestHandler<EdictProductCommand, Result<Unit>>
{
    private readonly IUnitOfWork _uow;

    public EdictProductCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<Unit>> Handle(EdictProductCommand request, CancellationToken cancellationToken)
    {
        var produto = await _uow.ProdutoRepository.GetByIdAsync(request.Id, cancellationToken);
        var categoria = await _uow.CategoriaRepository.GetByNameAsync(request.ProdutoDto.NomeCategoria, cancellationToken);

        if (produto is null)
            return Result<Unit>.FailureWithStatusCode("Produto não encontrado", HttpStatusCode.NotFound);

        produto.DataAlteracao = DateTime.UtcNow;
        produto.Nome = request.ProdutoDto.Nome;
        produto.UrlImagem = request.ProdutoDto.UrlImagem;
        produto.Valor = request.ProdutoDto.Valor;
        produto.QtdEstoque = request.ProdutoDto.QtdEstoque;
        produto.Categoria = categoria ?? new Categoria { Nome = request.ProdutoDto.Nome };
        produto.Ativo = request.ProdutoDto.Ativo ?? true;

        await _uow.CommitAsync(cancellationToken);
        return Result<Unit>.SuccessWithStatusCode(HttpStatusCode.NoContent);
    }
}
