namespace Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public int QtdEstoque { get; set; }
    public string? UrlImagem { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public Guid IdCategoria { get; set; }
    public virtual Categoria Categoria { get; set; } = null!;
    public bool Ativo { get; set; }
}
