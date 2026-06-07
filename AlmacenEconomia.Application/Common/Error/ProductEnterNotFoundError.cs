using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductEnterNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Esa entrada no esta registrada";
}