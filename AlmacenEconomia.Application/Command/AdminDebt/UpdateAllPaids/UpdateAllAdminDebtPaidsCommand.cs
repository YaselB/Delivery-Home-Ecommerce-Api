using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdateAllPaids;
public class UpdateAllAdminDebtPaidsCommand : IRequest<Result<Unit>>
{
    public required string AdminId { get ; set ;}
}