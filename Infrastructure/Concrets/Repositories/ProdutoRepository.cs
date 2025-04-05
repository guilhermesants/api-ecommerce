using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Concrets.Repositories;

public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
{
    public ProdutoRepository(EcommerceContext dbContext) : base(dbContext)
    { }

    public async Task<Produto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<(IEnumerable<Produto> produtos, int totalItens)> ObterProdutosPorFiltroAsync(string? categoria, 
                                                                                                    decimal? precoMinimo, 
                                                                                                    decimal? precoMaximo, 
                                                                                                    bool? ativo,
                                                                                                    int pagina,
                                                                                                    int qtdPagina,
                                                                                                    CancellationToken cancellationToken = default)
                                {
        var query = GetAll().Include(c => c.Categoria).AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoria))
            query = query.Where(p => p.Categoria.Nome.ToLower() == categoria.ToLower());

        if (precoMinimo.HasValue)
            query = query.Where(p => p.Valor >= precoMinimo.Value);

        if (precoMaximo.HasValue)
            query = query.Where(p => p.Valor <= precoMaximo.Value);

        if (ativo.HasValue)
            query = query.Where(p => p.Ativo == ativo.Value);

        var total = await query.CountAsync(cancellationToken);

        var produtos = await query
            .Skip((pagina - 1) * qtdPagina)
            .Take(qtdPagina)
            .ToListAsync(cancellationToken);

        return (produtos, total);
    }
}
