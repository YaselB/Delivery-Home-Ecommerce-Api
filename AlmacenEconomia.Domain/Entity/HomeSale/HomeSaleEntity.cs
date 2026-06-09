using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.HomeSaleDetails;
using AlmacenEconomia.Domain.Events.HomeSale.Create;

namespace AlmacenEconomia.Domain.Entity.HomeSale;
public class HomeSaleEntity : GenericEntity<HomeSaleEntity>
{
    public double Total {get ; set ;}
    public List<HomeSaleDetailsEntity> HomeSaleDetailsEntities {get ; set ;} = new List<HomeSaleDetailsEntity>();

    public static HomeSaleEntity Create(double total)
    {
        var homeSaleEntity = new HomeSaleEntity
        {
            Total = total
        };
        var CreateHomeSaleDomainEvent = new CreateHomeSaleEvent(homeSaleEntity.Id , homeSaleEntity.Total);
        homeSaleEntity.AddDomainEvent(CreateHomeSaleDomainEvent);
        return homeSaleEntity;
    }
    public void UpdateTotal(double total)
    {
        Total = total;
        UpdatedAt = DateTime.UtcNow;
        
    }
}