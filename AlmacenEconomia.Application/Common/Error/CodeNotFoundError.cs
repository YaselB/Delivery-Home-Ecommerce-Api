using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class CodeNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Usted no ha generado el codigo aun , por favor generelo";
}