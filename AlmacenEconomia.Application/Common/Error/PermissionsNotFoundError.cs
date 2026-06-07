using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class PermissionsNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;
    public string Message => "Algunos permisos enviados , no existen";
}