using AlmacenEconomia.Application.Interfaces.Error;
using AlmacenEconomia.Application.Interfaces.Result;

namespace AlmacenEconomia.Application.Common.ResultWithoutT;

public class Result : IResult
{
    public bool IsSuccess {get;}

    public bool IsFailure => !IsSuccess;

    public IError? error {get;}
    protected Result(bool IsSuccess , IError? error)
    {
        this.IsSuccess = IsSuccess;
        this.error = error;
    }
    public static Result Success() => new(true ,null);
    public static Result Failure(IError error) => new(false , error);
}