using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.Interface.Generic;
using AlmacenEconomia.Domain.Entity.Generic;
using AutoMapper;
using MediatR;


namespace AlmacenEconomia.Application.Command.Generic.Update;

public class UpdateGenericEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, Result<Unit>>
where TEntity : GenericEntity<TEntity>, new()
where TCommand : UpdateGenericEntityCommand<TEntity>
{
    protected readonly IGenericRepository<TEntity> repository;
    protected readonly IMapper mapper;
    public UpdateGenericEntityCommandHandler(IGenericRepository<TEntity> generic , IMapper mapper)
    {
        this.repository = generic;
        this.mapper = mapper;
    }
    public virtual async Task<Result<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetById(request.Id , cancellationToken);
        if(entity == null)
        {
            return Result<Unit>.Failure(new GenericEntityNotFoundError());
        }
        mapper.Map(request, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}