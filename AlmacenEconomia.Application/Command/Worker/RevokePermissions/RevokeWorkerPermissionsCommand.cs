using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Command.Worker.RevokePermissions;
public class RevokeWorkerPermissionsCommand : UpdateGenericEntityCommand<WorkerEntity>
{
    public required List<string> Permissions {get ; set ;}
}