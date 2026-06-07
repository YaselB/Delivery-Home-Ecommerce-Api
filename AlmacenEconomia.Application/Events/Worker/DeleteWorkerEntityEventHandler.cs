using AlmacenEconomia.Domain.Events.Worker.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Worker;

public class DeleteWorkerEntityEventHandler : INotificationHandler<DeleteWorkerEntityEvent>
{
    private readonly ILogger<DeleteWorkerEntityEventHandler> logger;
    public DeleteWorkerEntityEventHandler(ILogger<DeleteWorkerEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(DeleteWorkerEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha eliminado el trabajador con email: "+notification.WorkerEmail+" y id: "+notification.WorkerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}