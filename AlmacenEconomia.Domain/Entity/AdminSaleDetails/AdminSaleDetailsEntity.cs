using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Domain.Entity.AdminSaleDetails;
public class AdminSaleDetailsEntity : GenericEntity<AdminSaleDetailsEntity>
{
    public string ProductId {get ; set ;} = string.Empty;
    public ProductEntity? ProductEntity {get ; set ;}
    public string AdminSaleId {get ; set ;} = string.Empty;
    public AdminSaleEntity? AdminSaleEntity {get ; set ;}
    public double Expensive {get ; set ;}
    public double Quantity {get ; set ;}
    public static AdminSaleDetailsEntity Create(string productId , string adminSaleId , double expensive , double quantity)
    {
        var adminSaleDetail = new AdminSaleDetailsEntity
        {
            ProductId = productId,
            AdminSaleId = adminSaleId,
            Expensive = expensive,
            Quantity = quantity
        };
        return adminSaleDetail;
    }
    public void UpdateQuantity(double quantity)
    {
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdateExpensive(double expensive)
    {
        Expensive = expensive;
        UpdatedAt = DateTime.UtcNow;
    }

}