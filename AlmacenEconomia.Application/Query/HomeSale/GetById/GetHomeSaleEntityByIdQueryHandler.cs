using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.HomeSale.GetById;

public class GetHomeSaleEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<HomeSaleEntity, GetHomeSaleEntityByIdQuery, HomeSaleResultDto>
{
    private readonly IHomeSaleRepository homeSaleRepository;
    private readonly ILogger<HomeSaleEntity> logger;
    private readonly IMapper mapper;
    public GetHomeSaleEntityByIdQueryHandler(IHomeSaleRepository genericRepository, IMapper mapper , ILogger<HomeSaleEntity> logger) : base(genericRepository, mapper)
    {
        homeSaleRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<HomeSaleResultDto?>> Handle(GetHomeSaleEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var homeSale = await homeSaleRepository.GetById(request.Id , cancellationToken);
        if(homeSale == null)
        {
            logger.LogWarning("La salida para la casa con id: "+request.Id+" no esta registrada");
            return Result<HomeSaleResultDto?>.Failure(new HomeSaleNotFoundError());
        }
        var entityBack = mapper.Map<HomeSaleResultDto>(homeSale);
        return Result<HomeSaleResultDto?>.Success(entityBack);
    }
}