using AlmacenEconomia.Application.Features.Combo.ProductItem;
using AlmacenEconomia.Application.Features.Combo.ResultDto;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Entity.ComboDetails;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.Combo.ComboProfile;
public class ComboProfile : Profile
{
    public ComboProfile()
    {
        CreateMap<ComboEntity , ComboResultDto>()
        .ForMember(dest => dest.ProductItems , opt => opt.MapFrom(src => src.ComboDetails))
        .ReverseMap();
        CreateMap<ComboDetailsEntity ,ProductItemDto>()
        .ForMember(dest => dest.Id , opt => opt.MapFrom(src => src.ProductId))
        .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
        .ForMember(dest => dest.Quantity , opt => opt.MapFrom(src => src.Quantity))
        .ForMember(dest => dest.Unity , opt => opt.MapFrom(src => src.Product != null ? src.Product.Unity : string.Empty))
        .ReverseMap();
    }
}