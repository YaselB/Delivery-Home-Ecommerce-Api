using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.Delete;

public class DeleteProductEntityEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string ProductName {get ;}
    public string ProductId {get ;}
    public DeleteProductEntityEvent(string productName , string productId){
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductName = productName;
        ProductId = productId;
    }
}