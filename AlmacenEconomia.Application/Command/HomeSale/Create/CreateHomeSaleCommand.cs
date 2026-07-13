using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Features.HomeSale.CreateDto;
using AlmacenEconomia.Domain.Entity.HomeSale;

namespace AlmacenEconomia.Application.Command.HomeSale.Create;
public class CreateHomeSaleCommand : CreateGenericEntityCommand<HomeSaleEntity>
{
    public List<CreateHomeSaleDto> HomeSaleDtos {get ; set ;} = new List<CreateHomeSaleDto>();
}