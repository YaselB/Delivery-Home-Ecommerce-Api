using AlmacenEconomia.Domain.Events.Combo.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Combo;

public class DeleteComboEntityEventHandler : INotificationHandler<DeleteComboEntityEvent>
{
    private readonly ILogger<DeleteComboEntityEventHandler> logger;
    public DeleteComboEntityEventHandler(ILogger<DeleteComboEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(DeleteComboEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha eliminado el combo: "+notification.ComboName+" con id: "+notification.ComboId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}