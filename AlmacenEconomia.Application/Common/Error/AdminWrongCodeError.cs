using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminWrongCodeError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "El codigo que proporciona no es el correcto";
}