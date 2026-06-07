using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Command.Product.UpdateUrl;
public class UpdateProductUrlCommand : UpdateGenericEntityCommand<ProductEntity>
{
    public required string Url {get ; set ;}
}