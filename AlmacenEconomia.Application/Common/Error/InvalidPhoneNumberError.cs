using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class InvalidPhoneNumberError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "El número de teléfono no tiene un formato válido. Debe incluir código de país y entre 5 y 15 dígitos.";
}