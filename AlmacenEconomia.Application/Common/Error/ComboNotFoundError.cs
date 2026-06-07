using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ComboNotFoundError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Ese combo no está registrado";
}