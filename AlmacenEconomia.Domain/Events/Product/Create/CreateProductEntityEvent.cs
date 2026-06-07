using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.Create;

public class CreateProductEntityEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string ProductId {get ;}
    public string ProductName {get ;}
    public CreateProductEntityEvent(string productId , string productName)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductId = productId;
        ProductName = productName;
    }
}