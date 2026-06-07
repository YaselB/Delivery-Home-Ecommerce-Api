using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class ProductSectionNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "La section de ese producto no esta registrada ,por favor contacte al admin";
}