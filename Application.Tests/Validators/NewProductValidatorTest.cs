using Application.Dtos;
using Application.UseCases.AdicionarProduto;
using FluentValidation.TestHelper;

namespace Application.Tests.Validators;

public class NewProductValidatorTest
{
    private readonly NewProductCommandValidator _validator;

    public NewProductValidatorTest()
    {
        _validator = new NewProductCommandValidator();
    }

    [Fact]
    public void Deve_Retornar_CampoObrigatorio_Quando_Nome_Vazio()
    {
        var produtoDto = new ProdutoDto("", 5.00m, 10, "Higiene");

        var result = _validator.TestValidate(new NewProductCommand(produtoDto));

        result.ShouldHaveValidationErrorFor("ProdutoDto.Nome")
              .WithErrorMessage("O nome do produto é obrigatório.");
    }

    [Fact]
    public void Deve_Retornar_Valor_Maior_Que_Zero_Quando_Valor_Zero()
    {
        var produtoDto = new ProdutoDto("Shampo", 0m, 10, "Higiene");

        var result = _validator.TestValidate(new NewProductCommand(produtoDto));

        result.ShouldHaveValidationErrorFor("ProdutoDto.Valor")
              .WithErrorMessage("O valor do produto deve ser maior que zero.");
    }

    [Fact]
    public void Deve_Retornar_Quantidade_Maior_Que_Zero_Quando_Quantidade_Zero()
    {
        var produtoDto = new ProdutoDto("Shampo", 10m, 0, "Higiene");

        var result = _validator.TestValidate(new NewProductCommand(produtoDto));

        result.ShouldHaveValidationErrorFor("ProdutoDto.QtdEstoque")
              .WithErrorMessage("A quantide disponível do produto deve ser maior que zero.");
    }

    [Fact]
    public void Deve_Retornar_CampoObrigatorio_Quando_Categoria_For_Vazia()
    {
        var produtoDto = new ProdutoDto("Shampo", 10m, 10, "");

        var result = _validator.TestValidate(new NewProductCommand(produtoDto));

        result.ShouldHaveValidationErrorFor("ProdutoDto.NomeCategoria")
              .WithErrorMessage("O nome da categoria é obrigatório.");
    }

    [Fact]
    public void Nao_Deve_Retornar_Erro_Quando_Produto_Valido()
    {
        var produtoDto = new ProdutoDto("Shampo", 10m, 10, "Higiene");

        var result = _validator.TestValidate(new NewProductCommand(produtoDto));

        result.ShouldNotHaveAnyValidationErrors();
    }
}
