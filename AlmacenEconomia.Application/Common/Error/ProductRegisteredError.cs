using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Ese producto ya ha sido registrado";
}