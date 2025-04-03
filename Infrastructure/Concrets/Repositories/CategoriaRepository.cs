using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Concrets.Repositories;

internal class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(EcommerceContext dbContext) : base(dbContext) { }

    public async Task<Categoria?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    
    public async Task<Categoria?> GetByNameAsync(string nome, CancellationToken cancellationToken = default)
        =>  await Find(x => x.Nome.ToLower() == nome.ToLower()).FirstOrDefaultAsync(cancellationToken);
    
}