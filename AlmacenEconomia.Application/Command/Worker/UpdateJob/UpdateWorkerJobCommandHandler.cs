using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.WorkersType;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Worker.UpdateJob;

public class UpdateWorkerJobCommandHandler : UpdateGenericEntityCommandHandler<WorkerEntity, UpdateWorkerJobCommand>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;
    public UpdateWorkerJobCommandHandler(IWorkerRepository generic, IMapper mapper , ILogger<WorkerEntity> logger) : base(generic, mapper)
    {
        workerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateWorkerJobCommand request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetById(request.Id , cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("El trabajador con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new WorkerNotFoundError());
        }
        var jobs = WorkersType.AllWorkers.ToList();
        if (!jobs.Contains(request.NewJob))
        {
            logger.LogWarning("Se esta intentando asignar un tipo de trabajador no registrado");
            return Result<Unit>.Failure(new JobNotFoundError());
        }
        worker.UpdateJob(request.NewJob);
        await workerRepository.UpdateAsync(worker , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}