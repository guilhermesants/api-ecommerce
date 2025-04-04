using FluentValidation;

namespace Application.UseCases.RemoverProduto;

public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
                          .WithMessage("O identificador do produto é obrigatório");
    }
}
