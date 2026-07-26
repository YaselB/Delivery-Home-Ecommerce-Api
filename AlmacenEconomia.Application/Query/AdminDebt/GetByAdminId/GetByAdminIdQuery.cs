using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using MediatR;

namespace AlmacenEconomia.Application.Query.AdminDebt.GetByAdminId;
public class GetByAdminIdQuery : IRequest<Result<IReadOnlyList<AdminDebtDto>>>
{
    public required string AdminId { get ; set ;}
}