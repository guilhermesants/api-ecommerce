using Application.Dtos;
using FluentValidation;

namespace Application.Common.Validators;

public class ProdutoDtoValidator : AbstractValidator<ProdutoDto>
{
    public ProdutoDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor do produto deve ser maior que zero.");

        RuleFor(x => x.QtdEstoque)
            .GreaterThan(0).WithMessage("A quantide disponível do produto deve ser maior que zero.");

        RuleFor(x => x.NomeCategoria)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.");
    }
}

