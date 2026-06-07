using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "El producto no esta registrado";
}