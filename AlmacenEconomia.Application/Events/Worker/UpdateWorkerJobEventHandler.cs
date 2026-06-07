using AlmacenEconomia.Domain.Events.Worker.UpdateJob;
using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Worker;

public class UpdateWorkerJobEventHandler : INotificationHandler<UpdateWorkerJobEvent>
{
    private readonly ILogger<UpdateWorkerJobEventHandler> logger;
    public UpdateWorkerJobEventHandler(ILogger<UpdateWorkerJobEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateWorkerJobEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el trabajo del correo : "+notification.WorkerEmail+" y id: "+notification.WorkerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}