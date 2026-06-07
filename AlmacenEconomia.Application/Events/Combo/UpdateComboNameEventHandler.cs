using AlmacenEconomia.Domain.Events.Combo.UpdateName;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Combo;

public class UpdateComboNameEventHandler : INotificationHandler<UpdateComboNameEvent>
{
    private readonly ILogger<UpdateComboNameEventHandler> logger;
    public UpdateComboNameEventHandler(ILogger<UpdateComboNameEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateComboNameEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el nombre del combo con id: "+notification.ComboId+" y con nuevo nombre: "+notification.Name+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}