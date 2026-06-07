using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class InvalidPhoneCodeError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "El código de telefono es incorrecto ";
}