using AlmacenEconomia.Application.Features.Offer.OfferResult;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Query.Offer.GetAll;
public class GetAllOfferEntityQuery : GetAllGenericEntityQuery<OfferEntity, OfferResultDto>
{
    
}