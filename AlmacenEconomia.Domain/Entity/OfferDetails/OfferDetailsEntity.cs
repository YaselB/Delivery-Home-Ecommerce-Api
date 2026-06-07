using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Domain.Entity.OfferDetails;
public class OfferDetailsEntity : GenericEntity<OfferDetailsEntity>
{
    public string ProductId {get ; set ;} = string.Empty;
    public ProductEntity? ProductEntity {get ; set ;}
    public string OfferId {get ; set ;} = string.Empty;
    public OfferEntity? OfferEntity {get ; set ;}
    public double Quantity {get ; set ;}
    public static OfferDetailsEntity Create(string offerId , string productId , double quantity)
    {
        var offerDetail = new OfferDetailsEntity
        {
            OfferId = offerId,
            ProductId = productId,
            Quantity = quantity
        };
        return offerDetail;
    }
}