using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Concrets.Repositories;

public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
{
    public ProdutoRepository(EcommerceContext dbContext) : base(dbContext)
    { }

    public async Task<Produto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    
}
