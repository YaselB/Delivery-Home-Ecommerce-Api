using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class JobNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;
    public string Message => "El puesto asignado al trabajador no esta registrado";
}