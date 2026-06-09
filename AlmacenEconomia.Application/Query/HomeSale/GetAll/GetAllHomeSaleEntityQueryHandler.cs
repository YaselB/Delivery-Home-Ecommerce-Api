using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.HomeSale.GetAll;

public class GetAllHomeSaleEntityQueryHandler : GetAllGenericEntityQueryHandler<HomeSaleEntity, GetAllHomeSaleEntityQuery, HomeSaleResultDto>
{
    private readonly IHomeSaleRepository homeSaleRepository;
    private readonly IMapper mapper;
    public GetAllHomeSaleEntityQueryHandler(IHomeSaleRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        homeSaleRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<IReadOnlyList<HomeSaleResultDto>>> Handle(GetAllHomeSaleEntityQuery request, CancellationToken cancellationToken)
    {
        var entities = await homeSaleRepository.GetAll(cancellationToken);
        var entitiesBack = new List<HomeSaleResultDto>();
        foreach(var i in entities)
        {
            entitiesBack.Add(mapper.Map<HomeSaleResultDto>(i));
        }
        return Result<IReadOnlyList<HomeSaleResultDto>>.Success(entitiesBack);
    }
}