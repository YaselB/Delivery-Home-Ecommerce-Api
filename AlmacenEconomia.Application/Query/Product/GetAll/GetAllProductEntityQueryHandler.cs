using AlmacenEconomia.Application.Features.Product.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.Product.GetAll;

public class GetAllProductEntityQueryHandler : GetAllGenericEntityQueryHandler<ProductEntity, GetAllProductEntityQuery, ProductResultDto>
{
    public GetAllProductEntityQueryHandler(IGenericRepository<ProductEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
    }
}