using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.ProductEnter.UpdateEndDate;

public class UpdateEndDateEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ProductEnterId {get ;}
    public DateTime? EndDate {get ;}
    public UpdateEndDateEvent(string productEnterId , DateTime? endDate)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductEnterId = productEnterId;
        EndDate = endDate;
    }
}