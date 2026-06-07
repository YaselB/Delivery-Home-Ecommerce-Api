using FluentValidation;

namespace AlmacenEconomia.Application.Command.Admin.Create;
public class CreateAdminCommandValidator : AbstractValidator<CreateAdminEntityCommand>
{
    public CreateAdminCommandValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("El correo no puede estar vacio")
        .EmailAddress().WithMessage("El correo no es valido")
        .MaximumLength(100).WithMessage("El correo no puede superar los 100 caracteres.");
    }
}