using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ComboRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "El combo ya ha sido registrado";
}