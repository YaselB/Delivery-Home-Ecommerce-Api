using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminDebtNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Ese prestamo no está registrado";
}