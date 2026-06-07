using AlmacenEconomia.Domain.Events.Worker.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Worker;

public class CreateWorkerEntityEventHandler : INotificationHandler<CreateWorkerEntityEvent>
{
    private readonly ILogger<CreateWorkerEntityEventHandler> logger;
    public CreateWorkerEntityEventHandler(ILogger<CreateWorkerEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateWorkerEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un trabajador con email: "+notification.WorkerEmail+" y id: "+notification.WorkerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}