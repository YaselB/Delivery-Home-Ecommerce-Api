using AlmacenEconomia.Domain.Events.AdminSale.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.AdminSale;

public class CreateAdminSaleEventHandler : INotificationHandler<CreateAdminSaleEvent>
{
    private readonly ILogger<CreateAdminSaleEventHandler> logger;
    public CreateAdminSaleEventHandler(ILogger<CreateAdminSaleEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateAdminSaleEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado una salida para el admin: "+notification.AdminId+" con un gasto de:"+notification.Total+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}