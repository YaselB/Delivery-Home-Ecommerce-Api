using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.HomeSale.Create;

public class CreateHomeSaleEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string HomeSaleId {get ;}
    public double Total {get ;}
    public CreateHomeSaleEvent(string homeSaleId , double total)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        HomeSaleId = homeSaleId;
        Total = total;
    }
}