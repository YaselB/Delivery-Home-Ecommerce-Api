using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Offer.UpdateProductList;

public class UpdateProductListEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string OfferId {get ;}
    public string OfferName {get ;}
    public UpdateProductListEvent(string offerId , string offerName)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        OfferId = offerId;
        OfferName = offerName;
    }
}