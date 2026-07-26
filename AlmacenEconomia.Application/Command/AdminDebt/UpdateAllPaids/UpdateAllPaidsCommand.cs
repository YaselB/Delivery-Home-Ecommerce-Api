using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.AdminDebt;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdateAllPaids;
public class UpdateAllPaidsCommand : UpdateGenericEntityCommand<AdminDebtEntity>
{
    public required string AdminId { get ; set ;}
}