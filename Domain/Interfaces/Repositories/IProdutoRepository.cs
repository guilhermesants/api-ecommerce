using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IProdutoRepository : IRepositoryBase<Produto>
{
    Task<Produto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
