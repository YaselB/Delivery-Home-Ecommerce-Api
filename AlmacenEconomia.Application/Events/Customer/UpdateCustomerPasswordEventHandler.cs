using AlmacenEconomia.Domain.Events.Customer.UpdatePassword;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Customer;

public class UpdateCustomerPasswordEventHandler : INotificationHandler<UpdateCustomerPasswordEvent>
{
    private readonly ILogger<UpdateCustomerPasswordEventHandler> logger;
    public UpdateCustomerPasswordEventHandler(ILogger<UpdateCustomerPasswordEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateCustomerPasswordEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El cliente con email: "+notification.CustomerEmail+" y id: "+notification.CustomerId+" ha actualizado su contraseña en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}