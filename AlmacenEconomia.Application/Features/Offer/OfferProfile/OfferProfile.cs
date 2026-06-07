using AlmacenEconomia.Application.Features.Offer.OfferResult;
using AlmacenEconomia.Application.Features.Offer.ProductItem;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Domain.Entity.OfferDetails;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.Offer.OfferProfile;
public class OfferProfile : Profile
{
    public OfferProfile()
    {
        CreateMap<OfferEntity , OfferResultDto>()
        .ForMember(dest => dest.Products , opt => opt.MapFrom(src => src.OffersDetails))
        .ReverseMap();
        CreateMap<OfferDetailsEntity ,ProductItemsOffer>()
        .ForMember(dest => dest.Id , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Id : string.Empty))
        .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Name : string.Empty))
        .ForMember(dest => dest.Unity , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Unity : string.Empty))
        .ForMember(dest => dest.Quantity , opt => opt.MapFrom(src => src.Quantity))
        .ReverseMap();
    }
}