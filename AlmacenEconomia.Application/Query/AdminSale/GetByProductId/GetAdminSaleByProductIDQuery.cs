using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminSale.Dto;
using MediatR;

namespace AlmacenEconomia.Application.Query.AdminSale.GetByProductId;
public class GetAdminSaleByProductIdQuery : IRequest<Result<IReadOnlyList<AdminSaleResultDto>>>
{
    public required string ProductId {get ; set ;}
}