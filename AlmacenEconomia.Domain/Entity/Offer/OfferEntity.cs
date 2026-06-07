using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.OfferDetails;
using AlmacenEconomia.Domain.Events.Offer.Create;
using AlmacenEconomia.Domain.Events.Offer.UpdateName;
using AlmacenEconomia.Domain.Events.Offer.UpdatePrice;

namespace AlmacenEconomia.Domain.Entity.Offer;
public class OfferEntity : GenericEntity<OfferEntity>
{
    public string Name {get ; set ;} = string.Empty;
    public double Price {get ; set ;}
    public List<OfferDetailsEntity> OffersDetails = new List<OfferDetailsEntity>();
    public static OfferEntity Create(string name ,double price)
    {
        var offer = new OfferEntity
        {
            Name = name,
            Price = price
        };
        var CreateOfferDomainEvent = new CreateOfferEntityEvent(offer.Id , offer.Name);
        offer.AddDomainEvent(CreateOfferDomainEvent);
        return offer;
    }
    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
        var updateNameDomainEvent = new UpdateOfferNameEvent(Name , Id);
        AddDomainEvent(updateNameDomainEvent);
    }
    public void UpdatePrice(double price)
    {
        Price = price;
        UpdatedAt = DateTime.UtcNow;
        var updatePriceDomainEvent = new UpdateOfferPriceEvent(Id ,Price);
        AddDomainEvent(updatePriceDomainEvent);
    }
}