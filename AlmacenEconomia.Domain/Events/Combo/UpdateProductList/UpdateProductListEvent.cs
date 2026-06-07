using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Combo.UpdateProductList;

public class UpdateProductListEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get ;}
    public string ComboId {get ; }
    public string ComboName {get ;}
    public UpdateProductListEvent(string comboId , string comboName)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ComboId = comboId;
        ComboName = comboName;
    }
}