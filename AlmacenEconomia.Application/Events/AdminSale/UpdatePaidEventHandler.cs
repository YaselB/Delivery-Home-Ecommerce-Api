using AlmacenEconomia.Domain.Events.AdminSale.UpdatePaid;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.AdminSale;

public class UpdatePaidEventHandler : INotificationHandler<UpdatePaidEvent>
{
    private readonly ILogger<UpdatePaidEventHandler> logger;
    public UpdatePaidEventHandler(ILogger<UpdatePaidEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdatePaidEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("La salida de admin con id: "+notification.AdminSaleId+" ha sido actualizado su pago a :"+notification.Paid+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}