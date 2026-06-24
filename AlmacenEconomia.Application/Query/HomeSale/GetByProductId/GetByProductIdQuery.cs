using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using MediatR;

namespace AlmacenEconomia.Application.Query.HomeSale.GetProductIdQuery;
public class GetByProductIdQuery : IRequest<Result<IReadOnlyList<HomeSaleResultDto>>>
{
    public required string ProductId {get ; set ;}
}