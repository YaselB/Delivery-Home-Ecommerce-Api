using AlmacenEconomia.Application.Features.Worker.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Query.Worker.GetAll;
public class GetAllWorkerEntityQuery : GetAllGenericEntityQuery<WorkerEntity , WorkerResultDto>{}