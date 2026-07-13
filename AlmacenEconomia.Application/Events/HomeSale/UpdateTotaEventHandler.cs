using AlmacenEconomia.Domain.Events.HomeSale.UpdateTotal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.HomeSale;

public class UpdateTotalEventHandler : INotificationHandler<UpdateTotalEvent>
{
    private readonly ILogger<UpdateTotalEventHandler> logger;
    public UpdateTotalEventHandler(ILogger<UpdateTotalEventHandler> logger){
        this.logger = logger;
    }
    public Task Handle(UpdateTotalEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la salida para la casa con id: "+notification.HomeSaleId+" con un nuevo gasto de :"+notification.Total+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}