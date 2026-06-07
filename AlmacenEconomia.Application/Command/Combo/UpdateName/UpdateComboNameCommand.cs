using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Combo;

namespace AlmacenEconomia.Application.Command.Combo.UpdateName;
public class UpdateComboNameCommand : UpdateGenericEntityCommand<ComboEntity>
{
    public required string Name {get ; set ;}
}