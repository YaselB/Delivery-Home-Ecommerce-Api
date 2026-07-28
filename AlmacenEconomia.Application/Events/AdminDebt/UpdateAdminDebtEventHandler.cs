using AlmacenEconomia.Domain.Events.AdminDebt.UpdatePaid;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.AdminDebt;

public class UpdateAdminDebtEventHandler : INotificationHandler<UpdatePaidEvent>
{
    private readonly ILogger<UpdateAdminDebtEventHandler> logger;
    public UpdateAdminDebtEventHandler(ILogger<UpdateAdminDebtEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdatePaidEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha hecho el pago de una deuda del admin con id: "+notification.AdminId+" con un monto de : "+notification.Paid+" en la fecha : "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}