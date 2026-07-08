using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.AdminSale.Create;

public class CreateAdminSaleEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string AdminId {get ;}
    public double Total {get ;}
    public CreateAdminSaleEvent(string adminId , double total)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        AdminId = adminId;
        Total = total;
    }
}