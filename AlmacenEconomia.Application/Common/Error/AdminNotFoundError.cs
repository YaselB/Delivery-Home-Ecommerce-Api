using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Ese admin no esta registrado";
}