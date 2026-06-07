using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenEconomia.Application.Command.Generic.Create;

public class CreateGenericEntityCommandHandler<T , TCommand> : IRequestHandler<TCommand, Result<Unit>>
where T : GenericEntity<T>, new ()
where TCommand : CreateGenericEntityCommand<T>
{
    protected readonly IGenericRepository<T> generic;
    protected readonly IMapper mapper;
    public CreateGenericEntityCommandHandler(IGenericRepository<T> repository , IMapper mapper)
    {
        this.mapper = mapper;
        generic = repository;
    }
    public virtual async Task<Result<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = new T();
        mapper.Map(request , entity);
        await generic.AddAsync(entity);
        return Result<Unit>.Success(Unit.Value);
    }
}