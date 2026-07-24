using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class SectionNotFoundError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "La seccion que intenta poner no es válida";
}