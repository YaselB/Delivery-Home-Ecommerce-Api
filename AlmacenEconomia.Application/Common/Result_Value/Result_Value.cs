using AlmacenEconomia.Application.Common.ResultWithoutT;
using AlmacenEconomia.Application.Interfaces.Error;

namespace AlmacenEconomia.Application.Common.Result_Value;

public class Result<T> : Result
{
    public T? Value {get ;}
    protected Result(bool IsSuccess, IError? error , T? value) : base(IsSuccess, error)
    {
        Value = value;
    }
    public static Result<T> Success(T value) => new Result<T>(true , null , value);
    public new static Result<T> Failure(IError error) => new Result<T>(false , error , default);
}