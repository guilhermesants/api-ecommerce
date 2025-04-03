using FluentValidation;

namespace Application.UseCases.AdicionarProduto;

public class NewProductCommandValidator : AbstractValidator<NewProductCommand>
{
    public NewProductCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor do produto deve ser maior que zero");

        RuleFor(x => x.QtdEstoque)
            .GreaterThan(0).WithMessage("O quantide disponível do produto deve ser maior que zero");

    }
}

