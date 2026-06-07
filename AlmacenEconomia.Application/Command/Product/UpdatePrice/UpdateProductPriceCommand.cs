using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Command.Product.UpdatePrice;
public class UpdateProductPriceCommand : UpdateGenericEntityCommand<ProductEntity>
{
    public required double Price {get ; set ;}
}