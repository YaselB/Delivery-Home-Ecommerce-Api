using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AutoMapper;
using MediatR;

namespace AlmacenEconomia.Application.Query.HomeSale.GetProductIdQuery;

public class GetProductIdQueryHandler : IRequestHandler<GetByProductIdQuery, Result<IReadOnlyList<HomeSaleResultDto>>>
{
    private readonly IHomeSaleRepository homeSaleRepository;
    private readonly IMapper mapper;
    public GetProductIdQueryHandler(IHomeSaleRepository homeSaleRepository , IMapper mapper)
    {
        this.homeSaleRepository = homeSaleRepository;
        this.mapper = mapper;
    }
    public async Task<Result<IReadOnlyList<HomeSaleResultDto>>> Handle(GetByProductIdQuery request, CancellationToken cancellationToken)
    {
        var list = await homeSaleRepository.GetByProductId(request.ProductId , cancellationToken);
        var backList = new List<HomeSaleResultDto>();
        foreach(var i in list)
        {
            backList.Add(mapper.Map<HomeSaleResultDto>(i));
        }
        return Result<IReadOnlyList<HomeSaleResultDto>>.Success(backList);
    }
}