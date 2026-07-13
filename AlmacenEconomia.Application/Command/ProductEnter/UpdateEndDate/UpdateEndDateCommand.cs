using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateEndDate;
public class UpdateEndDateCommand : UpdateGenericEntityCommand<ProductEnterEntity>
{
    public required DateTime EndDate {get ; set ;}
}