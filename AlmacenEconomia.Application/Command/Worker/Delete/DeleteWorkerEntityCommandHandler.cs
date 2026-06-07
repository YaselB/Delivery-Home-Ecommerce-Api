using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Worker;
using AlmacenEconomia.Domain.Events.Worker.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Worker.Delete;

public class DeleteWorkerEntityCommandHandler : DeleteGenericEntityCommandHandler<WorkerEntity, DeleteWorkerEntityCommand>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;

    public DeleteWorkerEntityCommandHandler(IWorkerRepository genericRepository , ILogger<WorkerEntity> logger) : base(genericRepository)
    {
        workerRepository = genericRepository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(DeleteWorkerEntityCommand request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetById(request.Id , cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("El trabajador con Id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new WorkerNotFoundError());
        }
        var DeleteWorkerDomainEvent = new DeleteWorkerEntityEvent(worker.Id , worker.Email);
        worker.AddDomainEvent(DeleteWorkerDomainEvent);
        await workerRepository.DeleteAsync(worker ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}