using System.Data;
using FluentValidation;

namespace AlmacenEconomia.Application.Command.Customer.Create;
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerEntityCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("El correo no puede estar vacio")
        .EmailAddress().WithMessage("Por favor ,introduzca una direccion de correo valida")
        .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres");
    }
}