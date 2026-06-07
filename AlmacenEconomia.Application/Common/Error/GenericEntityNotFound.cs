using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class GenericEntityNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;
    public string Message => "No se encuetra la entidad";
}