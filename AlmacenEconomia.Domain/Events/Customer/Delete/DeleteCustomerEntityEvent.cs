using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Customer.Delete;

public class DeleteCustomerEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get;}
    public string CustomerId {get;}
    public string CustomerEmail {get ;}
    public DeleteCustomerEntityEvent(string customerId , string customerEmail)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        CustomerId = customerId;
        CustomerEmail = customerEmail;
    }
}