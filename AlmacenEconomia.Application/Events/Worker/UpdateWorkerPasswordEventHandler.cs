using AlmacenEconomia.Domain.Events.Worker.UpdatePassword;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Worker;

public class UpdateWorkerPasswordEventHandler : INotificationHandler<UpdateWorkerPasswordEvent>
{
    private readonly ILogger<UpdateWorkerPasswordEventHandler> logger;
    public UpdateWorkerPasswordEventHandler(ILogger<UpdateWorkerPasswordEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateWorkerPasswordEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la contraseña del trabajador con email: "+notification.WorkerEmail+" y con id: "+notification.WorkerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}