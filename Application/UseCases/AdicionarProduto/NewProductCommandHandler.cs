using Application.Common.Responses;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.AdicionarProduto;

public class NewProducCommandHandler : IRequestHandler<NewProductCommand, Result<NewProductCommandResponse>>
{
    private readonly IUnitOfWork _uow;

    public NewProducCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<NewProductCommandResponse>> Handle(NewProductCommand request, CancellationToken cancellationToken)
    {

        var categoria = await _uow.CategoriaRepository.GetByNameAsync(request.NomeCategoria, cancellationToken);

        var novoProduto = new Produto
        {
            Nome = request.Nome,
            Valor = request.Valor,
            QtdEstoque = request.QtdEstoque,
            Categoria = categoria is null ? new Categoria
            {
                Nome = "Higiene"
            } : categoria
        };

        _uow.ProdutoRepository.Add(novoProduto);
        await _uow.CommitAsync(cancellationToken);

        return Result<NewProductCommandResponse>.Success(
            new NewProductCommandResponse(novoProduto.Id)
            );
    }
}