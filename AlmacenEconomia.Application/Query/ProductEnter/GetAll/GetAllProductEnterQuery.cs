using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Query.ProductEnter.GetAll;
public class GetAllProductEnterQuery : GetAllGenericEntityQuery<ProductEnterEntity , ProductEnterResultDto>
{
    
}