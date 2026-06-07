using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class CustomerEntityNotFoundError : IError
{
    public int Code => StatusCodes.Status400BadRequest;
    public string Message => "El cliente no esta registrado";
}