using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminDebtPaidError : IError
{
    public int Code => StatusCodes.Status400BadRequest;
    public string Message => "Algunos prestamos ya han sido pagados";
}