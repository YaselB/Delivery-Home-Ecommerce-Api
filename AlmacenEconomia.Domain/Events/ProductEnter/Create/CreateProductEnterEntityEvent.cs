using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.ProductEnter.Create;

public class CreateProductEnterEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get;}
    public string ProductEnterId {get ;}
    public string ProductEnterCode {get ;}
    public CreateProductEnterEntityEvent (string productEnterId , string productEnterCode)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductEnterId = productEnterId;
        ProductEnterCode = productEnterCode;
    }
}