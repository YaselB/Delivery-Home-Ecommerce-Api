using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.ProductEnter.UpdateCode;

public class UpdateProductEnterCode : IDomainEvent, INotification
{
    public string id {get ; }

    public DateTime CreatedAt {get ;}
    public string ProductEnterId {get ;}
    public string ProductEnterCode {get ;}
    public UpdateProductEnterCode(string productEnterId , string productEnterCode)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductEnterId = productEnterId;
        ProductEnterCode = productEnterCode;
    }
}