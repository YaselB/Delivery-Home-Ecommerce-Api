using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductsNotRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Algunos productos no estan registrados";
}