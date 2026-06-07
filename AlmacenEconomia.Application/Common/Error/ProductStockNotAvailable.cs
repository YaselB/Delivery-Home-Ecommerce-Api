using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductStockNotAvailable : IError
{
    public int Code => StatusCodes.Status400BadRequest;
    public string Message => "El producto no contiene la cantidad necesaria para satisfacer el pedido";
}