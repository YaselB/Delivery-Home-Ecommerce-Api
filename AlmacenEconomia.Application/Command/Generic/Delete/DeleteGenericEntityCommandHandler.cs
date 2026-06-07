using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;

namespace AlmacenEconomia.Application.Command.Generic.Delete;

public class DeleteGenericEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, Result<Unit>>
where TEntity : GenericEntity<TEntity>, new()
where TCommand : DeleteGenericEntityCommand<TEntity>
{
    protected readonly IGenericRepository<TEntity> repository;
    public DeleteGenericEntityCommandHandler(IGenericRepository<TEntity> genericRepository)
    {
        this.repository = genericRepository;
    }
    public virtual async Task<Result<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetById(request.Id , cancellationToken);
        if(entity == null)
        {
            return Result<Unit>.Failure(new GenericEntityNotFoundError());
        }
        await repository.DeleteAsync(entity , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}