using Application.Common.Responses;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace Application.UseCases.AdicionarProduto;

public class NewProducCommandHandler : HandlerBase, IRequestHandler<NewProductCommand, Result<NewProductCommandResponse>>
{
    public NewProducCommandHandler(IUnitOfWork uow) : base(uow) { }

    public async Task<Result<NewProductCommandResponse>> Handle(NewProductCommand request, CancellationToken cancellationToken)
    {
        var categoria = await _uow.CategoriaRepository.GetByNameAsync(request.ProdutoDto.NomeCategoria, cancellationToken);

        var novoProduto = new Produto
        {
            Nome = request.ProdutoDto.Nome,
            Valor = request.ProdutoDto.Valor,
            QtdEstoque = request.ProdutoDto.QtdEstoque,
            Categoria = categoria  ?? new Categoria { Nome = "Higiene" },
            UrlImagem = request.ProdutoDto.UrlImagem,
            Ativo = request.ProdutoDto.Ativo ?? true
        };

        _uow.ProdutoRepository.Add(novoProduto);
        await _uow.CommitAsync(cancellationToken);

        return Result<NewProductCommandResponse>.SuccessWithStatusCode(
            new NewProductCommandResponse(novoProduto.Id),
            HttpStatusCode.Created
        );
    }
}