using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Features.AdminSale.CreateDto;
using AlmacenEconomia.Domain.Entity.AdminSale;

namespace AlmacenEconomia.Application.Command.AdminSale.Create;
public class CreateAdminSaleEntityCommand : CreateGenericEntityCommand<AdminSaleEntity>
{
    public string AdminId {get ; set ;} = string.Empty;
    public List<CreateAdminSaleDto> CreateAdminSaleDtos{get ; set ;} = new List<CreateAdminSaleDto>(); 
}