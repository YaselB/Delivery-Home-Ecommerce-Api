using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.ProductEnter.UpdateEnterDate;

public class UpdateEnterDateEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ProductEnterId {get ;}
    public DateTime EnterDate {get ;}
    public UpdateEnterDateEvent(string productEnterId , DateTime enterDate)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductEnterId = productEnterId;
        EnterDate = enterDate;
    }
}