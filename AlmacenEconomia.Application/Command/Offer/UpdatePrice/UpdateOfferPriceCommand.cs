using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Command.Offer.UpdatePrice;
public class UpdateOfferPriceCommand : UpdateGenericEntityCommand<OfferEntity>
{
    public required double Price {get ; set ;}
}