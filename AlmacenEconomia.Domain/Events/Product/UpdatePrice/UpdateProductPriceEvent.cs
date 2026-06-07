using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.UpdatePrice;

public class UpdateProductPriceEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string ProductName {get ;}
    public double NewPrice {get;}
    public UpdateProductPriceEvent(string productName , double newPrice)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductName = productName;
        NewPrice = newPrice;
    }
}