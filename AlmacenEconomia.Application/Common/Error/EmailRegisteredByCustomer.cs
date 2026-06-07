using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class EmailRegisteredByCustomer : IError
{
    public int Code => StatusCodes.Status400BadRequest;
    public string Message => "Ese correo ya ha sido registrado por un cliente ,por favor utilice otro correo";
}