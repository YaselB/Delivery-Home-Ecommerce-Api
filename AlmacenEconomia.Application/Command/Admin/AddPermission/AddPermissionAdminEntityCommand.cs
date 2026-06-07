using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Admin;

namespace AlmacenEconomia.Application.Command.Admin.AddPermission;
public class AddPermissionAdminEntity : UpdateGenericEntityCommand<AdminEntity>
{
    public required List<string> Permissions {get ; set ;} 
}