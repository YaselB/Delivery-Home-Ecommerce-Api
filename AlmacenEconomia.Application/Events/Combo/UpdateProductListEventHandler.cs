using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Events.Combo.UpdateProductList;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Combo;

public class UpdateProductListEventHandler : INotificationHandler<UpdateProductListEvent>
{
    private readonly ILogger<UpdateProductListEventHandler> logger;
    public UpdateProductListEventHandler(ILogger<UpdateProductListEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateProductListEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se han actualizado los productos del combo: "+notification.ComboName+" con el id: "+notification.ComboId+" en la fecha: "+notification.CreatedAt+" y con idEvent:"+notification.id);
        return Task.CompletedTask;
    }
}