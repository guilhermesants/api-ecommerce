using Application.Common.Validators;
using FluentValidation;

namespace Application.UseCases.EditarProduto;

public class EdictProductCommandValidator : AbstractValidator<EdictProductCommand>
{
    public EdictProductCommandValidator()
    {
        RuleFor(x => x.ProdutoDto)
            .SetValidator(new ProdutoDtoValidator());
    }
}
