using FluentValidation;

namespace AlmacenEconomia.Application.Command.Worker.Create;
public class CreateWorkerCommandValidator : AbstractValidator<CreateWorkerEntityCommand>
{
    public CreateWorkerCommandValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("El correo no puede estar vacio")
        .EmailAddress().WithMessage("Entre una direccion de correo valida")
        .MaximumLength(100).WithMessage("El correo no debe exceder los 100 caracteres");
    }
}