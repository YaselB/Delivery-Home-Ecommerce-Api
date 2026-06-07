using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.UpdateProductQuantityEvent;

public class UpdateProductQuantityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ProductName {get ;}
    public double NewQuantity {get ;}
    public UpdateProductQuantityEvent(string productName , double newQuantity)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductName = productName;
        NewQuantity = newQuantity;
    }
}