using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductEnterRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "La entrada con ese codigo ya ha sido registrada";
}