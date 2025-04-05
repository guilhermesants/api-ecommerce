using FluentValidation;

namespace Application.UseCases.UploadImagemProduto;

public class UploadImageProductCommandValidator : AbstractValidator<UploadImageProductCommand>
{
    public UploadImageProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
                  .WithMessage("O identificador do produto é obrigatório");
    }
}
