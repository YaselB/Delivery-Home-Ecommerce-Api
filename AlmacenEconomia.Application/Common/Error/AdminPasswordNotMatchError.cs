using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdminPasswordNotMatchError : IError
{
    public int Code => StatusCodes.Status400BadRequest;
    public string Message => "Las contraseñas no coinciden ,por favor vuelva a introducir su contraseña";
}