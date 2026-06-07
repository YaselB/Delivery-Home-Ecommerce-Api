using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;

namespace AlmacenEconomia.Application.Query.Generic.GetById;
public class GetGenericEntityByIdQuery<TEntity ,TResultDto> : IRequest<Result<TResultDto?>>
where TEntity : GenericEntity<TEntity>
where TResultDto : class
{
    public required string Id {get ; set ;}
}