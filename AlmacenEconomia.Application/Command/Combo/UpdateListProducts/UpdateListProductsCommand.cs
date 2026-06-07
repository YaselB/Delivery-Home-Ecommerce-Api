using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Features.Combo.CreateDto;
using AlmacenEconomia.Domain.Entity.Combo;

namespace AlmacenEconomia.Application.Command.Combo.UpdateListProducts;
public class UpdateListProductsCommands : UpdateGenericEntityCommand<ComboEntity>
{
    public required List<CreateComboDto> CreateComboDtos {get ; set ;}
}