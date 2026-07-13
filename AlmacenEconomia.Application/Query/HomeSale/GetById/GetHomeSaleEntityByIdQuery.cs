using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.HomeSale;

namespace AlmacenEconomia.Application.Query.HomeSale.GetById;
public class GetHomeSaleEntityByIdQuery : GetGenericEntityByIdQuery<HomeSaleEntity , HomeSaleResultDto>
{
    
}