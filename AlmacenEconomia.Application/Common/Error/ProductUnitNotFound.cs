using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductUnitNotFound : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "La unidad que desea entra no es válida";
}