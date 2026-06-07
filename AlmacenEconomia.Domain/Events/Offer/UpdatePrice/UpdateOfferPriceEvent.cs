using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Offer.UpdatePrice;

public class UpdateOfferPriceEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string OfferId {get ;}
    public double Price {get ;}
    public UpdateOfferPriceEvent(string offerId , double price)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        OfferId = offerId;
        Price = price;
    }
}