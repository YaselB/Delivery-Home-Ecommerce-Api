using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class CustomerRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Ya existe un cliente registrado con ese correo";
}