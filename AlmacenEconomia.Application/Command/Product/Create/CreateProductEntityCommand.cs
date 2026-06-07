using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Command.Product.Create;
public class CreateProductEntityCommand : CreateGenericEntityCommand<ProductEntity>
{
    public string Name {get ; set ;} = string.Empty;
    public string Section {get ; set ; } = string.Empty;
    public string Url {get ; set ; } = string.Empty;
    public double Price {get ; set ;} 
    public string Unity {get ; set ; } = string.Empty;
}