using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminSale.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.AdminSale.GetByProductId;

public class GetAdminSaleByProductIdQueryHandler : IRequestHandler<GetAdminSaleByProductIdQuery, Result<IReadOnlyList<AdminSaleResultDto>>>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    private readonly IMapper mapper;
    public GetAdminSaleByProductIdQueryHandler(IAdminSaleRepository adminSaleRepository , IMapper mapper)
    {
        this.adminSaleRepository = adminSaleRepository;
        this.mapper = mapper;
    }
    public async Task<Result<IReadOnlyList<AdminSaleResultDto>>> Handle(GetAdminSaleByProductIdQuery request, CancellationToken cancellationToken)
    {
        var adminSale = await adminSaleRepository.GetByProductId(request.ProductId , cancellationToken);
        var salesBack = new List<AdminSaleResultDto>();
        foreach(var i in adminSale)
        {
            salesBack.Add(mapper.Map<AdminSaleResultDto>(i));
        }
        return Result<IReadOnlyList<AdminSaleResultDto>>.Success(salesBack);
    }
}