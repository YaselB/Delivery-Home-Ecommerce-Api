using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Customer.RevokePermissions;

public class RevokePermissionCustomerEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get; }
    public string CustomerEmail {get ;}
    public string CustomerId {get ;}
    public RevokePermissionCustomerEvent(string customerId , string customerEmail)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        CustomerId = customerId;
        CustomerEmail = customerEmail;
    }
}