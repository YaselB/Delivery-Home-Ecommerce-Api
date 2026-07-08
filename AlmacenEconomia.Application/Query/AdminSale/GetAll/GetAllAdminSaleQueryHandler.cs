using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminSale.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.AdminSale.GetAll;

public class GetAllAdminSaleQueryHandler : GetAllGenericEntityQueryHandler<AdminSaleEntity, GetAllAdminSaleQuery, AdminSaleResultDto>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    private readonly IMapper mapper;
    public GetAllAdminSaleQueryHandler(IAdminSaleRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        adminSaleRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<IReadOnlyList<AdminSaleResultDto>>> Handle(GetAllAdminSaleQuery request, CancellationToken cancellationToken)
    {
        var adminSales = await adminSaleRepository.GetAll(cancellationToken);
        var salesBack = new List<AdminSaleResultDto>();
        foreach(var i in adminSales)
        {
            salesBack.Add(mapper.Map<AdminSaleResultDto>(i));
        }
        return Result<IReadOnlyList<AdminSaleResultDto>>.Success(salesBack);
    }
}