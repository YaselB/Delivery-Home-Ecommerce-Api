using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Command.Worker.UpdateJob;
public class UpdateWorkerJobCommand : UpdateGenericEntityCommand<WorkerEntity>
{
    public required string NewJob {get ; set ;}
}