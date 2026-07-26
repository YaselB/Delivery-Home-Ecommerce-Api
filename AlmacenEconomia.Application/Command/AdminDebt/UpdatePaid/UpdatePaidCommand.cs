using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.AdminDebt;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdatePaid;
public class UpdatePaidCommand : UpdateGenericEntityCommand<AdminDebtEntity>
{
    public required string AdminId { get ; set ;}
    public required List<string> DebtIds {get ; set ;}
}