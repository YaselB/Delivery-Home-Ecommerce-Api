using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminSaleNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Esa salida para un administrador no se encuentra";
}