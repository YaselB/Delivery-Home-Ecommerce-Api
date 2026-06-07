using AlmacenEconomia.Domain.Events.Customer.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Customer;

public class CreateCustomerEntityEventHandler : INotificationHandler<CreateCustomerEntityEvent>
{
    private readonly ILogger<CreateCustomerEntityEventHandler> logger;
    public CreateCustomerEntityEventHandler(ILogger<CreateCustomerEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateCustomerEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un nuevo cliente con email: "+notification.Email+" y con id: "+notification.CustomerId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}