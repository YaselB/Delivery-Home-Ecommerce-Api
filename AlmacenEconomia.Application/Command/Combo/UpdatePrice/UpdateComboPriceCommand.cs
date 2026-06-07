using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Combo;

namespace AlmacenEconomia.Application.Command.Combo.UpdatePrice;
public class UpdateComboPriceCommand : UpdateGenericEntityCommand<ComboEntity>
{
    public required double Price {get ; set ;}
}