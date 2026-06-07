using AlmacenEconomia.Application.Features.Offer.OfferResult;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Query.Offer.GetById;
public class GetOfferEntityByIdQuery : GetGenericEntityByIdQuery<OfferEntity , OfferResultDto>
{
    
}