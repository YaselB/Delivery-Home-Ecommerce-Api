using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.ProductEnter.ProductEnterProfile;
public class ProductEnterProfile : Profile
{
    public ProductEnterProfile()
    {
        CreateMap<ProductEnterEntity , ProductEnterResultDto>()
        .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Name : string.Empty))
        .ForMember(dest => dest.StockQuantity ,opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Quantity : 0))
        .ForMember(dest => dest.Unity , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Unity : string.Empty))
        .ReverseMap();
    }
}