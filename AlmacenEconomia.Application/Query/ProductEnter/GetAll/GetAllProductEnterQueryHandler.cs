using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.ProductEnter.GetAll;

public class GetAllProductEnterQueryHandler : GetAllGenericEntityQueryHandler<ProductEnterEntity, GetAllProductEnterQuery, ProductEnterResultDto>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly IMapper mapper;
    public GetAllProductEnterQueryHandler(IProductEnterRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        productEnterRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<IReadOnlyList<ProductEnterResultDto>>> Handle(GetAllProductEnterQuery request, CancellationToken cancellationToken)
    {
        var products = await productEnterRepository.GetAll(cancellationToken);
        var listBack = new List<ProductEnterResultDto>();
        foreach(var i in products)
        {
            listBack.Add(mapper.Map<ProductEnterResultDto>(i));
        }
        return Result<IReadOnlyList<ProductEnterResultDto>>.Success(listBack);
    }
}