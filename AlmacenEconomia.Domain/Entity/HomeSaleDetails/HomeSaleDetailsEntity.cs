using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Domain.Entity.HomeSaleDetails;
public class HomeSaleDetailsEntity : GenericEntity<HomeSaleDetailsEntity>
{
    public string HomeSaleId {get ; set ;} = string.Empty;
    public HomeSaleEntity? HomeSaleEntity {get ; set ;}
    public string ProductId {get ; set ;} = string.Empty;
    public ProductEntity ? ProductEntity {get ; set ;}
    public double Quantity {get ; set ;}
    public double Expense {get ; set ;}
    public static HomeSaleDetailsEntity Create(string homeSaleId , string productId , double quantity , double expense)
    {
        var entity = new HomeSaleDetailsEntity
        {
            HomeSaleId = homeSaleId,
            ProductId = productId,
            Quantity = quantity,
            Expense = expense
        };
        return entity;
    }
    public void UpdateQuantity(double quantity)
    {
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdateExpense(double expense)
    {
        Expense = expense;
        UpdatedAt = DateTime.UtcNow;
    }
}