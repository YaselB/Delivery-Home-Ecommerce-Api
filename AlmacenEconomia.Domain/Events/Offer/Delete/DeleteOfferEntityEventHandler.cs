using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Offer.Delete;

public class DeleteOfferEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string OfferId {get ;}
    public string OfferName {get ;}
    public DeleteOfferEntityEvent(string offerId , string offerName)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        OfferId = offerId;
        OfferName = offerName;
    }
}