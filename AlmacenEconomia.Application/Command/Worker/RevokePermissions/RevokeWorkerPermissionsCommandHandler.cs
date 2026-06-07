using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Worker.RevokePermissions;

public class RevokeWorkerPermissionsCommandHandler : UpdateGenericEntityCommandHandler<WorkerEntity, RevokeWorkerPermissionsCommand>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;
    public RevokeWorkerPermissionsCommandHandler(IWorkerRepository generic, IMapper mapper , ILogger<WorkerEntity> logger) : base(generic, mapper)
    {
        workerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(RevokeWorkerPermissionsCommand request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetById(request.Id , cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("El trabajador con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new WorkerNotFoundError());
        }
        worker.RevokePermission(request.Permissions);
        await workerRepository.UpdateAsync(worker);
        return Result<Unit>.Success(Unit.Value);
    }
}