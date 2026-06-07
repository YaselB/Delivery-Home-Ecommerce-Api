using AlmacenEconomia.Domain.Events.Worker.AddPermission;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Worker;

public class AddWorkerPermissionEventHandler : INotificationHandler<AddWorkerPermissionEvent>
{
    private readonly ILogger<AddWorkerPermissionEventHandler> logger;
    public AddWorkerPermissionEventHandler(ILogger<AddWorkerPermissionEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(AddWorkerPermissionEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se le han asignado permisos al trabajador con email: "+notification.WorkerEmail+" y con id: "+notification.WorkerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}