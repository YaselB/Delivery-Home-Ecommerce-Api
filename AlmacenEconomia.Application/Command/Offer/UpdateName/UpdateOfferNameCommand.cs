using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Command.Offer.UpdateName;
public class UpdateOfferNameCommand : UpdateGenericEntityCommand<OfferEntity>
{
    public required string Name {get ; set ;}
}