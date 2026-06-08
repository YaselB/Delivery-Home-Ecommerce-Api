using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.HomeSaleDetails;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Domain.Entity.HomeSale;
public class HomeSaleEntity : GenericEntity<HomeSaleEntity>
{
    public double Total {get ; set ;}
    public ICollection<HomeSaleDetailsEntity>? HomeSaleDetailsEntities {get ; set ;}
    public ProductEnterEntity? ProductEnter {get ; set ;}
    public string ProductEnterId {get ; set ;} = string.Empty;

}