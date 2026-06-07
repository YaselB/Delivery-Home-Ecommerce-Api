using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.UpdateUnity;

public class UpdateProductUnityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ProductName {get ;}
    public string NewUnity {get ;}
    public UpdateProductUnityEvent(string productName , string newUnity){
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductName = productName;
        NewUnity = newUnity;
    }
}