using FluentValidation;

namespace Application.UseCases.AdicionarProduto;

public class NewProductCommandValidator : AbstractValidator<NewProductCommand>
{
    public NewProductCommandValidator()
    {
        RuleFor(x => x.ProdutoDto.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório");

        RuleFor(x => x.ProdutoDto.Valor)
            .GreaterThan(0).WithMessage("O valor do produto deve ser maior que zero");

        RuleFor(x => x.ProdutoDto.QtdEstoque)
            .GreaterThan(0).WithMessage("O quantide disponível do produto deve ser maior que zero");

        RuleFor(x => x.ProdutoDto.NomeCategoria)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório");
    }
}

