using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Domain.Entity.AdminSaleDetails;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Events.AdminSale.Create;
using AlmacenEconomia.Domain.Events.AdminSale.UpdatePaid;
using AlmacenEconomia.Domain.Events.AdminSale.UpdateTotal;

namespace AlmacenEconomia.Domain.Entity.AdminSale;
public class AdminSaleEntity : GenericEntity<AdminSaleEntity>
{
    public double Total {get ; set ;}
    public string AdminId {get ; set ;} = string.Empty;
    public AdminEntity? AdminEntity {get ; set ;}
    public List<AdminSaleDetailsEntity> AdminSaleDetailsEntities {get ; set ;} = new List<AdminSaleDetailsEntity>();
    public bool Paid {get ; set ;} = false;
    public static AdminSaleEntity Create(double total ,string adminId)
    {
        var adminSale = new AdminSaleEntity
        {
            AdminId = adminId,
            Total = total,
            Paid = false
        };
        var createAdminSaleDomainEvent = new CreateAdminSaleEvent(adminSale.AdminId ,adminSale.Total);
        adminSale.AddDomainEvent(createAdminSaleDomainEvent);
        return adminSale;
    }
    public void UpdateTotal(double total)
    {
        Total = total;
        UpdatedAt = DateTime.UtcNow;
        var updateTotalDomainEvent = new UpdateTotalEvent(Id ,Total);
        AddDomainEvent(updateTotalDomainEvent);
    }
    public void UpdatePaid()
    {
        Paid = true;
        UpdatedAt = DateTime.UtcNow;
        var updatePaidDomainEvent = new UpdatePaidEvent(Id , Paid);
        AddDomainEvent(updatePaidDomainEvent);
    }
}