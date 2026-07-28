using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminsDebtNotFoundError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Algunos préstamos no están registrados";
}