using AlmacenEconomia.Domain.Events.Combo.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Combo;

public class CreateComboEntityEventHandler : INotificationHandler<CreateComboEntityEvent>
{
    private readonly ILogger<CreateComboEntityEventHandler> logger;
    public CreateComboEntityEventHandler(ILogger<CreateComboEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateComboEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un combo con id: "+notification.ComboId+" y con nombre: "+notification.ComboName+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}