using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class OfferNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Esa oferta no esta registrada";
}