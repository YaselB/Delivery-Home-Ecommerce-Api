using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Events.AdminDebt.Create;
using AlmacenEconomia.Domain.Events.AdminDebt.UpdatePaid;

namespace AlmacenEconomia.Domain.Entity.AdminDebt;
public class AdminDebtEntity : GenericEntity<AdminDebtEntity>
{
    public double Debt { get ; set ;}
    public bool Paid {get ; set ;}
    public string AdminId {get ; set ;} = string.Empty;
    public AdminEntity? Admin {get ; set ;}
    public static AdminDebtEntity Create(double debt , string adminId)
    {
        var adminDebt = new AdminDebtEntity
        {
            AdminId = adminId,
            Debt = debt,
            Paid = false
        };
        var createAdminDebtDomainEvent = new CreateAdminDebtEvent(adminDebt.Id , adminDebt.Debt);
        adminDebt.AddDomainEvent(createAdminDebtDomainEvent);
        return adminDebt;
    }
    public void UpdatePaid()
    {
        Paid = true;
        UpdatedAt = DateTime.UtcNow;
        var updatePaidDomainEvent = new UpdatePaidEvent(AdminId , Paid);
        AddDomainEvent(updatePaidDomainEvent);
    }
}