using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.ProductEnter.UpdateQuantity;

public class UpdateProductEnterQuantityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ProductEnterId {get ;}
    public double ProductEnterQuantity {get ;}
    public UpdateProductEnterQuantityEvent(string productEnterId , double productEnterQuantity)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductEnterId = productEnterId;
        ProductEnterQuantity = productEnterQuantity;
    }
}