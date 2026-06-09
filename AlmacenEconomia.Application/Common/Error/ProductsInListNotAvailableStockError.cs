using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductsInListNotAvailableStockError : IError 
{
    public int Code {get;}

    public string Message {get;}
    public ProductsInListNotAvailableStockError(string message)
    {
        Code = StatusCodes.Status400BadRequest;
        Message = message;
    }
}