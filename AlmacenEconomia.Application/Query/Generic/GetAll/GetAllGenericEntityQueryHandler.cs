using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;
using AutoMapper;
using AlmacenEconomia.Application.Repository.Generic;

namespace AlmacenEconomia.Application.Query.Generic.GetAll;

public class GetAllGenericEntityQueryHandler<TEntity, TQuery, TResultDto> : IRequestHandler<TQuery, Result<IReadOnlyList<TResultDto>>>
where TEntity : GenericEntity<TEntity>, new()
where TQuery : GetAllGenericEntityQuery<TEntity, TResultDto>
where TResultDto : class
{
    private readonly IGenericRepository<TEntity> genericRepository;
    private readonly IMapper mapper;
    public GetAllGenericEntityQueryHandler(IGenericRepository<TEntity> genericRepository , IMapper mapper)
    {
        this.genericRepository = genericRepository;
        this.mapper = mapper;
    }
    public virtual async Task<Result<IReadOnlyList<TResultDto>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var entities = await genericRepository.GetAll(cancellationToken);
        var entitiesBack = new List<TResultDto>();
        foreach(var i in entities)
        {
            var entityBack = mapper.Map<TResultDto>(i);
            entitiesBack.Add(entityBack);
        }
        return Result<IReadOnlyList<TResultDto>>.Success(entitiesBack);
    }
}