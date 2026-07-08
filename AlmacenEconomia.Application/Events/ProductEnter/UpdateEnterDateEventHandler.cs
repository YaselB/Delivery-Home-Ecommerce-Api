using AlmacenEconomia.Domain.Events.ProductEnter.UpdateEnterDate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.ProductEnter;

public class UpdateEnterDateEventHandler : INotificationHandler<UpdateEnterDateEvent>
{
    private readonly ILogger<UpdateEnterDateEventHandler> logger;
    public UpdateEnterDateEventHandler(ILogger<UpdateEnterDateEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateEnterDateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la entrada: "+notification.ProductEnterId+" con la nueva fecha de entrada: "+notification.EnterDate+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}