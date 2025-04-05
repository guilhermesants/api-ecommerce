using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICategoriaRepository : IRepositoryBase<Categoria>
{
    Task<Categoria?> GetByNameAsync(string nome, CancellationToken cancellationToken = default);
}
