using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class EqualsUnitiesError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "No puede cambiar la unidad de mediada por la misma que estaba registrada con ese producto";
}