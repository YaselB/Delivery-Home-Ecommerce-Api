using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Offer.UpdateName;

public class UpdateOfferNameEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string OfferName {get ; }
    public string OfferId {get ;}
    public UpdateOfferNameEvent(string offerName , string offerId)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        OfferName = offerName;
        OfferId = offerId;
    }
}