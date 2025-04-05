using Application.Common.Validators;
using FluentValidation;

namespace Application.UseCases.AdicionarProduto;

public class NewProductCommandValidator : AbstractValidator<NewProductCommand>
{
    public NewProductCommandValidator()
    {
        RuleFor(x => x.ProdutoDto)
            .SetValidator(new ProdutoDtoValidator());
    }
}

