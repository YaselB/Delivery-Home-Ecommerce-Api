using AlmacenEconomia.Domain.Events.ProductEnter.UpdateEndDate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.ProductEnter;

public class UpdateEndDateEventHandler : INotificationHandler<UpdateEndDateEvent>
{
    private readonly ILogger<UpdateEndDateEventHandler> logger;
    public UpdateEndDateEventHandler(ILogger<UpdateEndDateEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateEndDateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la entrada con id: "+notification.ProductEnterId+" con la nueva fecha de vencimiento: "+notification.EndDate+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}