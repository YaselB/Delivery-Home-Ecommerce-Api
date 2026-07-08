using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminSale.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.AdminSale.GetById;

public class GetAdminSaleByIdQueryHandler : GetGenericEntityByIdQueryHandler<AdminSaleEntity, GetAdminSaleByIdQuery, AdminSaleResultDto>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    private readonly ILogger<AdminSaleEntity> logger;
    private readonly IMapper mapper;
    public GetAdminSaleByIdQueryHandler(IAdminSaleRepository genericRepository, IMapper mapper , ILogger<AdminSaleEntity> logger) : base(genericRepository, mapper)
    {
        adminSaleRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<AdminSaleResultDto?>> Handle(GetAdminSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var adminSale = await adminSaleRepository.GetById(request.Id , cancellationToken);
        if(adminSale == null)
        {
            logger.LogWarning(" La salida para admin con id: "+request.Id+" no se encuentra");
            return Result<AdminSaleResultDto?>.Failure(new AdminSaleNotFoundError());
        }
        return Result<AdminSaleResultDto?>.Success(mapper.Map<AdminSaleResultDto>(adminSale));
    }
}