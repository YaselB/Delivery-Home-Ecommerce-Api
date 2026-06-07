using AlmacenEconomia.Domain.Events.Customer.AddPermissions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Customer;

public class AddPermissionsToCustomerEventHandler : INotificationHandler<AddPermissionsToCustomerEvent>
{
    private readonly ILogger<AddPermissionsToCustomerEventHandler> logger;
    public AddPermissionsToCustomerEventHandler(ILogger<AddPermissionsToCustomerEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(AddPermissionsToCustomerEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se le han otorgado permisos al cliente con email: "+notification.CustomerEmail+" y con id: "+notification.CustomerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}