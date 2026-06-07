using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Command.Product.UpdateUnity;
public class UpdateProductUnityCommand : UpdateGenericEntityCommand<ProductEntity>
{
    public required string Unity {get ; set ;}
}