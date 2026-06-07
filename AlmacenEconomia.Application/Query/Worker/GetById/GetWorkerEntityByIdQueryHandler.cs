using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Worker.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Worker.GetById;

public class GetWorkerEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<WorkerEntity, GetWorkerEntityByIdQuery, WorkerResultDto>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;
    private readonly IMapper mapper;
    public GetWorkerEntityByIdQueryHandler(IWorkerRepository genericRepository, IMapper mapper , ILogger<WorkerEntity> logger) : base(genericRepository, mapper)
    {
        workerRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<WorkerResultDto?>> Handle(GetWorkerEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetById(request.Id , cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("El trabajador con id: "+request.Id+" no esta registrado");
            return Result<WorkerResultDto?>.Failure(new WorkerNotFoundError());
        }
        var workerBack = mapper.Map<WorkerResultDto?>(worker);
        return Result<WorkerResultDto?>.Success(workerBack);
    }
}