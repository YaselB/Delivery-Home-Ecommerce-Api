using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Command.Worker.AddPermissions;
public class AddWorkerPermissionsCommand : UpdateGenericEntityCommand<WorkerEntity>{
    public required List<string> Permissions {get ; set ;}
}