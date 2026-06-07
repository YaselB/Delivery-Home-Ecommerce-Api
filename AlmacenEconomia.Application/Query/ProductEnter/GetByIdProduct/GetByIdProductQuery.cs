using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using MediatR;

namespace AlmacenEconomia.Application.Query.ProductEnter.GetByIdProduct;
public class GetByIdProductQuery : IRequest<Result<IReadOnlyList<ProductEnterResultDto>>>
{
    public required string ProductId {get ; set ;}
}