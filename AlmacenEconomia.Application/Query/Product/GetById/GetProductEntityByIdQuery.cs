using AlmacenEconomia.Application.Features.Product.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Query.Product.GetById;
public class GetProductEntityByIdQuery : GetGenericEntityByIdQuery<ProductEntity , ProductResultDto>
{
    
}