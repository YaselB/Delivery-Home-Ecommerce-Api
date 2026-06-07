using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Admin;

namespace AlmacenEconomia.Application.Command.Admin.RevokePermission;
public class RevokePermissionsAdminEntityCommand : UpdateGenericEntityCommand<AdminEntity>
{
    public required List<string> Permissions{ get ; set ;}
}