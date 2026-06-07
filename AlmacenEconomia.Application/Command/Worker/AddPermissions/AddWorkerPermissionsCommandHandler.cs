using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Worker.AddPermissions;

public class AddWorkerPermissionsCommandHandler : UpdateGenericEntityCommandHandler<WorkerEntity, AddWorkerPermissionsCommand>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;
    public AddWorkerPermissionsCommandHandler(IWorkerRepository generic, IMapper mapper , ILogger<WorkerEntity> logger) : base(generic, mapper)
    {
        workerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(AddWorkerPermissionsCommand request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetById(request.Id , cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("El trabajador con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new WorkerNotFoundError());
        }
        var invalidPermissions = request.Permissions.Where(p => !Permissions.AllWorkerPermissions.Contains(p));
        if (invalidPermissions.Any())
        {
            logger.LogWarning($"Permisos inválidos: {string.Join(", ", invalidPermissions)}");
            return Result<Unit>.Failure(new PermissionsNotFoundError());
        }
        worker.AddPermission(request.Permissions);
        await workerRepository.UpdateAsync(worker , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}