using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminDebtNotRegisteredAdminIdError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Algunos prestamos no estan asignados a ese administrador";
}