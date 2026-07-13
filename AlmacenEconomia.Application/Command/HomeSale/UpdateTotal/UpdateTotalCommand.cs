using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Features.HomeSale.CreateDto;
using AlmacenEconomia.Domain.Entity.HomeSale;

namespace AlmacenEconomia.Application.Command.HomeSale.UpdateTotal;
public class UpdateTotalCommand : UpdateGenericEntityCommand<HomeSaleEntity>
{
    public required List<CreateHomeSaleDto> CreateHomes {get ; set ;}
}