namespace Application.Dtos;

public record ProdutoDto
(
    string Nome,
    decimal Valor,
    int QtdEstoque,
    string NomeCategoria,
    string? UrlImagem,
    bool? Ativo
);