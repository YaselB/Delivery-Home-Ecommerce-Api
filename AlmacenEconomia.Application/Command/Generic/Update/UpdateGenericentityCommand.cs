using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.Generic.Update;
public class UpdateGenericEntityCommand<T> : IRequest<Result<Unit>>
{
    public required string Id {get ; set ;}
}