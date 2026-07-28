using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using MediatR;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdatePaid;
public class UpdateAdminDebtPaidCommand : IRequest<Result<Unit>>
{
    public required string AdminId { get ; set ;}
    public required List<string> DebtIds {get ; set ;}
}