using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Query.ProductEnter.GetbyId;
public class GetProductEnterByIdQuery : GetGenericEntityByIdQuery<ProductEnterEntity , ProductEnterResultDto>
{
    
}