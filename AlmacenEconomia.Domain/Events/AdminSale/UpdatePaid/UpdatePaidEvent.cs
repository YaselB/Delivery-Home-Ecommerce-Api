using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.AdminSale.UpdatePaid;

public class UpdatePaidEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string AdminSaleId {get ;}
    public bool Paid {get ;}
    public UpdatePaidEvent(string adminSaleId , bool paid)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        AdminSaleId = adminSaleId;
        Paid = paid;
    }
}