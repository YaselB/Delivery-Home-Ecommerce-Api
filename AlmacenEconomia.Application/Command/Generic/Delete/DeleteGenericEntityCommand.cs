using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;

namespace AlmacenEconomia.Application.Command.Generic.Delete;
public class DeleteGenericEntityCommand<TEntity> : IRequest<Result<Unit>>
where TEntity : GenericEntity<TEntity>
{
    public required string Id { get ; set ;}
}