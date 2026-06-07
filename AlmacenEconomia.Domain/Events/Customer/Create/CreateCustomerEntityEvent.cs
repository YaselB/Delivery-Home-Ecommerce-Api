using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Customer.Create;

public class CreateCustomerEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string Email {get ;}
    public string CustomerId {get ;}
    public CreateCustomerEntityEvent(string email ,string customerId)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        Email = email;
        CustomerId = customerId;
    }
}