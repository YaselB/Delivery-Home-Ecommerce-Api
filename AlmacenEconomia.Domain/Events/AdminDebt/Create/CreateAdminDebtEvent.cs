using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.AdminDebt.Create;

public class CreateAdminDebtEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string AdminId {get ;}
    public double Debt {get ;}
    public CreateAdminDebtEvent(string adminId , double debt)
    {
        AdminId = adminId;
        Debt = debt;
        CreatedAt = DateTime.Now;
        id = Guid.NewGuid().ToString();
    }

}