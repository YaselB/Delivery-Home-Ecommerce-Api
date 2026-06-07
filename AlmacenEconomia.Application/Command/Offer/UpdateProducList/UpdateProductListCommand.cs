using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Features.Offer.CreateOfferDetails;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Command.Offer.UpdateProductList;
public class UpdateProductListCommand : UpdateGenericEntityCommand<OfferEntity>
{
    public required List<CreateOfferDetailsDto> CreateOfferDetails {get ; set ;}
}