using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Domain.Entity.AdminDebt;

namespace AlmacenEconomia.Application.Command.AdminDebt.Create;
public class CreateAdminDebtCommand : CreateGenericEntityCommand<AdminDebtEntity>
{
    public string AdminId {get ; set ;} = string.Empty;
    public double Amount {get ; set ;}
}