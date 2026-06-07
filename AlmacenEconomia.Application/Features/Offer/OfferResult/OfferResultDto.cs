using AlmacenEconomia.Application.Features.Offer.ProductItem;

namespace AlmacenEconomia.Application.Features.Offer.OfferResult;
public class OfferResultDto
{
    public required string Id {get ; set ;}
    public required string Name {get ; set ;}
    public required double Price {get ; set ;}
    public required List<ProductItemsOffer> Products {get ; set ;}
}