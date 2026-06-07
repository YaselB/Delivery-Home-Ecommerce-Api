using AlmacenEconomia.Application.Interfaces.Error;

namespace AlmacenEconomia.Application.Interfaces.Result;
public interface IResult
{
    public bool IsSuccess{get ;}
    public bool IsFailure {get ;}
    public IError? error {get ;}
    
}