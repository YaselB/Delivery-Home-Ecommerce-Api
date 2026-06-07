using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class WrongCodeError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "El codigo proporcionado por el usuario es incorrecto";
}