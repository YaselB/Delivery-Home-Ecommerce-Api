using AlmacenEconomia.Domain.Events.AdminSale.UpdateTotal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.AdminSale;

public class UpdateTotalEventHandler : INotificationHandler<UpdateTotalEvent>
{
    private readonly ILogger<UpdateTotalEventHandler> logger;
    public UpdateTotalEventHandler(ILogger<UpdateTotalEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateTotalEvent notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("Se ha actualizado la salida de admin con id: "+notification.AdminSaleId+" con el nuevo total: "+notification.Total+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}