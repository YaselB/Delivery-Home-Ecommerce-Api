using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class AdmisSalesNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Algunas salidas no han sido encontradas";
}