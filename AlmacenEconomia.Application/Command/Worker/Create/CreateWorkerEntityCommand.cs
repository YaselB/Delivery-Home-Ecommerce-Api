using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Command.Worker.Create;
public class CreateWorkerEntityCommand : CreateGenericEntityCommand<WorkerEntity>
{
    public string Email {get ; set ; } = string.Empty;
    public string Password {get ; set ;} = string.Empty;
    public string job {get ; set ;} = string.Empty;
}