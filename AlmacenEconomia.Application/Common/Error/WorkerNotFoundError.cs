using AlmacenEconomia.Application.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenEconomia.Application.Common.Error;

public class WorkerNotFoundError : IError
{
    public int Code => StatusCodes.Status404NotFound;

    public string Message => "Ese trabajador no esta registrado";
}