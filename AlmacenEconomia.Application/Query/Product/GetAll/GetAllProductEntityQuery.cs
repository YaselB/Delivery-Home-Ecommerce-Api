using AlmacenEconomia.Application.Features.Product.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Query.Product.GetAll;
public class GetAllProductEntityQuery : GetAllGenericEntityQuery<ProductEntity , ProductResultDto>
{
    
}