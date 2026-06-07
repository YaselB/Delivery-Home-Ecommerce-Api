using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class EmailRegisteredByAdminError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Ese correo ya ha sido registrado , por favor introduzca otro";
}