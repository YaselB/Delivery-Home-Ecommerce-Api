using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ComboNameRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Ese nombre ya ha sido usado por otro combo";
}