using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.Interface.Generic;
using AlmacenEconomia.Domain.Entity.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenEconomia.Application.Query.Generic.GetById;

public class GetGenericEntityByIdQueryHandler<TEntity, TQuery, TResultDto> : IRequestHandler<TQuery, Result<TResultDto?>>
where TEntity : GenericEntity<TEntity>, new()
where TQuery : GetGenericEntityByIdQuery<TEntity, TResultDto>
where TResultDto : class
{
    private readonly IGenericRepository<TEntity> genericRepository;
    private readonly IMapper mapper;
    public GetGenericEntityByIdQueryHandler(IGenericRepository<TEntity> genericRepository ,IMapper mapper)
    {
        this.mapper = mapper;
        this.genericRepository = genericRepository;
    }
    public virtual async Task<Result<TResultDto?>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var entity = await genericRepository.GetById(request.Id , cancellationToken);
        if(entity == null)
        {
            return Result<TResultDto?>.Failure(new GenericEntityNotFoundError());
        }
        var entityBack = mapper.Map<TResultDto>(entity);
        return Result<TResultDto?>.Success(entityBack);
    }
}