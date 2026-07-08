using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Features.AdminSale.CreateDto;
using AlmacenEconomia.Domain.Entity.AdminSale;

namespace AlmacenEconomia.Application.Command.AdminSale.UpdateTotal;
public class UpdateAdminSaleTotalCommand : UpdateGenericEntityCommand<AdminSaleEntity>
{
    public required List<CreateAdminSaleDto> CreateAdminSaleDtos {get ; set ;}
}