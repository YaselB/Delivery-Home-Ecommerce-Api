using AlmacenEconomia.Application.Features.AdminSale.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.AdminSale;

namespace AlmacenEconomia.Application.Query.AdminSale.GetById;
public class GetAdminSaleByIdQuery : GetGenericEntityByIdQuery<AdminSaleEntity, AdminSaleResultDto>
{
    
}