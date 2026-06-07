using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateQuantity;
public class UpdateQuantityCommand : UpdateGenericEntityCommand<ProductEnterEntity>
{
    public required double Quantity {get ; set ;}
}