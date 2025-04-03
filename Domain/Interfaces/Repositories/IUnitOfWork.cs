namespace Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    Task CommitAsync(CancellationToken cancellationToken = default);
}
