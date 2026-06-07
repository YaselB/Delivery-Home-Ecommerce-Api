using AlmacenEconomia.Domain.Events.Worker.RevokePermission;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Worker;

public class RevokeWorkerPermissionEventHandler : INotificationHandler<RevokeWorkerPermissionEvent>
{
    private readonly ILogger<RevokeWorkerPermissionEventHandler> logger;
    public RevokeWorkerPermissionEventHandler(ILogger<RevokeWorkerPermissionEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(RevokeWorkerPermissionEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se le han revocado permisos al trabajador con email: "+notification.WorkerEmail+" y id: "+notification.WorkerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}