using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IProdutoRepository : IRepositoryBase<Produto>
{
    Task<Produto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Produto> produtos, int totalItens)> ObterProdutosPorFiltroAsync(string? categoria,
                                                                                       decimal? precoMinimo,
                                                                                       decimal? precoMaximo,
                                                                                       bool? ativo,
                                                                                       int pagina, 
                                                                                       int qtdPagina,
                                                                                       CancellationToken cancellationToken = default);
}
