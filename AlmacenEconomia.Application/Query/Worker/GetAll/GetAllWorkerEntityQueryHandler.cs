using AlmacenEconomia.Application.Features.Worker.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.Worker.GetAll;

public class GetAllWorkerEntityQueryHandler : GetAllGenericEntityQueryHandler<WorkerEntity, GetAllWorkerEntityQuery, WorkerResultDto>
{
    public GetAllWorkerEntityQueryHandler(IGenericRepository<WorkerEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
    }
}