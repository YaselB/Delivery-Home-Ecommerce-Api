using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Domain.Entity.ComboDetails;
public class ComboDetailsEntity : GenericEntity<ComboDetailsEntity>
{
    public string ComboId {get ; set ;} = string.Empty;
    public ComboEntity? Combo {get ; set ;}
    public string ProductId {get ; set ;} = string.Empty;
    public ProductEntity? Product {get ; set ;}
    public double Quantity {get ; set ;}
    public static ComboDetailsEntity Create(string comboId , string productId , double quantity)
    {
        var comboDetails = new ComboDetailsEntity
        {
            ComboId = comboId,
            ProductId = productId,
            Quantity = quantity
        };
        return comboDetails;
    }
    
}