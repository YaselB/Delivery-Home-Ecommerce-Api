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
}