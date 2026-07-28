using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.AdminDebt.UpdatePaid;

public class UpdatePaidEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string AdminId {get ;}
    public bool Paid {get ;}
    public UpdatePaidEvent(string adminId , bool paid)
    {
        AdminId = adminId;
        Paid = paid;
        CreatedAt = DateTime.Now;
        id = Guid.NewGuid().ToString();
    }
}