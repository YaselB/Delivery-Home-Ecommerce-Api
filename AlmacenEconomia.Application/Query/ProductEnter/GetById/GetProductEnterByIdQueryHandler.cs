using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.ProductEnter.GetbyId;

public class GetProductEnterByIdQueryHandler : GetGenericEntityByIdQueryHandler<ProductEnterEntity, GetProductEnterByIdQuery, ProductEnterResultDto>
{
    private readonly ILogger<ProductEnterEntity> logger;
    private readonly IProductEnterRepository productEnterRepository;
    private readonly IMapper mapper;
    public GetProductEnterByIdQueryHandler(IProductEnterRepository genericRepository, IMapper mapper , ILogger<ProductEnterEntity> logger) : base(genericRepository, mapper)
    {
        this.logger = logger;
        productEnterRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<ProductEnterResultDto?>> Handle(GetProductEnterByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productEnterRepository.GetById(request.Id ,cancellationToken);
        if(product == null)
        {
            logger.LogWarning("La entrada con id: "+request.Id+" no esta registrada");
            return Result<ProductEnterResultDto?>.Failure(new ProductEnterNotFoundError());
        }
        var productBack = mapper.Map<ProductEnterResultDto>(product);
        return Result<ProductEnterResultDto?>.Success(productBack);
    }
}