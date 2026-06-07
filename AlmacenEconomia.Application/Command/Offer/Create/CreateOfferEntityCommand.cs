using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Features.Offer.CreateOfferDetails;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Command.Offer.Create;
public class CreateOfferEntityCommand : CreateGenericEntityCommand<OfferEntity>
{
    public string Name {get ; set ;} = string.Empty;
    public double Price {get ; set ;}
    public List<CreateOfferDetailsDto> OfferDetails {get ; set ;} = new List<CreateOfferDetailsDto>();
}