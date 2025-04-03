using Domain.Interfaces.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Concrets.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EcommerceContext _context;

    public UnitOfWork(EcommerceContext context)
    {
        _context = context;
        this.CategoriaRepository = new CategoriaRepository(_context);
        this.ProdutoRepository = new ProdutoRepository(_context);
    }

    public ICategoriaRepository CategoriaRepository { get; }
    public IProdutoRepository ProdutoRepository { get; }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
