using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateEnterDate;
public class UpdateEnterDateCommand : UpdateGenericEntityCommand<ProductEnterEntity>
{
    public required DateTime EnterDate {get ; set ;}
}