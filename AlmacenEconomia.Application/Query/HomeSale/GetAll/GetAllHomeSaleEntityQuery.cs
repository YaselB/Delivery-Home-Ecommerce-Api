using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.HomeSale;

namespace AlmacenEconomia.Application.Query.HomeSale.GetAll;
public class GetAllHomeSaleEntityQuery : GetAllGenericEntityQuery<HomeSaleEntity , HomeSaleResultDto>
{
    
}