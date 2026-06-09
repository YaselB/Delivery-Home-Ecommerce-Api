using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class HomeSaleNotFoundError : IError
{
    public int Code => StatusCodes.Status400BadRequest;
    public string Message => "Esa salida para la casa no esta registrada";
}