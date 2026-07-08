using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.AdminSale.UpdateTotal;

public class UpdateTotalEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string AdminSaleId {get ;}
    public double Total {get ;}
    public UpdateTotalEvent(string adminSaleId , double total)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        AdminSaleId = adminSaleId;
        Total = total ;
    }
}