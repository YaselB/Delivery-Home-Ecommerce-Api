using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.UpdateUrl;

public class UpdateProductUrlEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get ;}
    public string ProductName {get;}
    public string NewUrl {get;}
    public UpdateProductUrlEvent(string productName , string newUrl)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductName = productName;
        NewUrl = newUrl;
    }
}