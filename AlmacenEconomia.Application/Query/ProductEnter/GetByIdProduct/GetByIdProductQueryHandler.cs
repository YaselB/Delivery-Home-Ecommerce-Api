using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.ProductEnter.GetByIdProduct;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, Result<IReadOnlyList<ProductEnterResultDto>>>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly IMapper mapper;
    public GetByIdProductQueryHandler(IProductEnterRepository productEnter , IMapper mapper)
    {
        productEnterRepository = productEnter;
        this.mapper = mapper;
    }
    public async Task<Result<IReadOnlyList<ProductEnterResultDto>>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productEnterRepository.GetByProductId(request.ProductId , cancellationToken);
        var listBack = new List<ProductEnterResultDto>();
        foreach(var i in product)
        {
            listBack.Add(mapper.Map<ProductEnterResultDto>(i));
        }
        return Result<IReadOnlyList<ProductEnterResultDto>>.Success(listBack);
    }
}