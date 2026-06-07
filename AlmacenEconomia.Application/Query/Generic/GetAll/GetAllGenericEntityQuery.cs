using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;

namespace AlmacenEconomia.Application.Query.Generic.GetAll;
public class GetAllGenericEntityQuery<TEntity , TResultDto> : IRequest<Result<IReadOnlyList<TResultDto>>>
where TEntity : GenericEntity<TEntity>, new ()
where TResultDto : class
{
    
}