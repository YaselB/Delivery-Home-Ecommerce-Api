using AlmacenEconomia.Domain.Events.Customer.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Customer;

public class DeleteCustomerEntityEventHandler : INotificationHandler<DeleteCustomerEntityEvent>
{
    private readonly ILogger<DeleteCustomerEntityEventHandler> logger;
    public DeleteCustomerEntityEventHandler(ILogger<DeleteCustomerEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(DeleteCustomerEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Ha sido eliminado el cliente con id: "+notification.CustomerId+" y email: "+notification.CustomerEmail+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}