using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Offer.Create;

public class CreateOfferEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string OfferId {get ;}
    public string OfferName {get ;}
    public CreateOfferEntityEvent(string offerId , string offerName)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        OfferId = offerId;
        OfferName = offerName;
    }
}