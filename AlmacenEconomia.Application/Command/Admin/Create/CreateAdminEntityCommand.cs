using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Domain.Entity.Admin;

namespace AlmacenEconomia.Application.Command.Admin.Create;
public class CreateAdminEntityCommand : CreateGenericEntityCommand<AdminEntity>
{
    public string Email {get ; set ;} = string.Empty;
    public string Password {get ; set ;} = string.Empty;
}