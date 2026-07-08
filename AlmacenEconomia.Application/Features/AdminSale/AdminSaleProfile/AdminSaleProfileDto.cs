using AlmacenEconomia.Application.Features.AdminSale.AdminSaleDetailsDto;
using AlmacenEconomia.Application.Features.AdminSale.Dto;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSaleDetails;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.AdminSale.AdminSaleProfile;
public class AdminSaleProfileDto : Profile
{
    public AdminSaleProfileDto()
    {
        CreateMap<AdminSaleEntity , AdminSaleResultDto>()
        .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.AdminEntity != null ? src.AdminEntity.Email : string.Empty))
        .ForMember(dest => dest.adminSaleDetailsResultDtos , opt => opt.MapFrom(src => src.AdminSaleDetailsEntities))
        .ReverseMap();
        CreateMap<AdminSaleDetailsEntity , AdminSaleDetailsResultDto>()
        .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Name : string.Empty))
        .ForMember(dest => dest.Unity , opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Unity : string.Empty))
        .ReverseMap();
    }
}