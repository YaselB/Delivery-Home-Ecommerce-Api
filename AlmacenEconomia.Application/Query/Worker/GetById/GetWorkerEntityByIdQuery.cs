using AlmacenEconomia.Application.Features.Worker.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Query.Worker.GetById;
public class GetWorkerEntityByIdQuery : GetGenericEntityByIdQuery<WorkerEntity , WorkerResultDto>
{
    
}