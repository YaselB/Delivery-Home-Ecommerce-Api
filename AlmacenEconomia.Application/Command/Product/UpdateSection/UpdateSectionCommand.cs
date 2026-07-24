using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Command.Product.UpdateSection;
public class UpdateSectionCommand : UpdateGenericEntityCommand<ProductEntity>
{
    public required string Section {get ; set ;}
}