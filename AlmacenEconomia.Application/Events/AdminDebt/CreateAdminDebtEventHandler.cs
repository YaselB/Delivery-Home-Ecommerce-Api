using AlmacenEconomia.Domain.Events.AdminDebt.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.AdminDebt;

public class CreateAdminDebtEventHandler : INotificationHandler<CreateAdminDebtEvent>
{
    private readonly ILogger<CreateAdminDebtEventHandler> logger;
    public CreateAdminDebtEventHandler(ILogger<CreateAdminDebtEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateAdminDebtEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un prestamo para el admin con id: "+notification.AdminId+" con un monto de :"+notification.Debt+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}