using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class OfferRegisteredError : IError
{
    public int Code => StatusCodes.Status400BadRequest;

    public string Message => "Existe una oferta registrada con ese nombre";
}