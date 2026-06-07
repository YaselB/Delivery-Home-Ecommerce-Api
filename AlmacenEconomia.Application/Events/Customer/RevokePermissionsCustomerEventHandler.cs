using AlmacenEconomia.Domain.Events.Customer.RevokePermissions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Customer;

public class RevokePermissionsCustomerEventHandler : INotificationHandler<RevokePermissionCustomerEvent>
{
    private readonly ILogger<RevokePermissionsCustomerEventHandler> logger;
    public RevokePermissionsCustomerEventHandler(ILogger<RevokePermissionsCustomerEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(RevokePermissionCustomerEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se le han revocado permisos al cliente con email: "+notification.CustomerEmail+" y id: "+notification.CustomerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}