using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ConvertToUnitError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "No se puede convertir a unidades";
}