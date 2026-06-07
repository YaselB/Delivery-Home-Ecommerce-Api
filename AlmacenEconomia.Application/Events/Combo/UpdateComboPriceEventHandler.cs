using AlmacenEconomia.Domain.Events.Combo.UpdatePrice;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Combo;

public class UpdateComboPriceEventHandler : INotificationHandler<UpdateComboPriceEvent>
{
    private readonly ILogger<UpdateComboPriceEventHandler> logger;
    public UpdateComboPriceEventHandler(ILogger<UpdateComboPriceEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateComboPriceEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el precio del combo: "+notification.ComboName+" con el nuevo precio: "+notification.ComboPrice+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}