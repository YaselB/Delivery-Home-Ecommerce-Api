using AlmacenEconomia.Application.Features.Product.Dto;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.Product.ProductProfile;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity ,ProductResultDto>()
        .ReverseMap();
    }
}