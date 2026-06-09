using AlmacenEconomia.Application.Features.HomeSale.HomeSaleDetails;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AlmacenEconomia.Domain.Entity.HomeSaleDetails;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.HomeSale.HomeSaleProfile;
public class HomeSaleProfile : Profile
{
    public HomeSaleProfile()
    {
        CreateMap<HomeSaleEntity , HomeSaleResultDto>()
        .ForMember(dest => dest.HomeSaleDetails , opt => opt.MapFrom(src => src.HomeSaleDetailsEntities))
        .ReverseMap();
        CreateMap<HomeSaleDetailsEntity , HomeSaleDetailsResultDto>()
        .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Name : string.Empty))
        .ForMember(dest => dest.ProductId , opt => opt.MapFrom(src => src.ProductId))
        .ForMember(dest => dest.Quantity , opt => opt.MapFrom(src => src.Quantity))
        .ForMember(dest => dest.Unity , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Unity : string.Empty))
        .ReverseMap();
    }
}