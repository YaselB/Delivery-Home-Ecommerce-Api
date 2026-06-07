using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Combo.UpdatePrice;

public class UpdateComboPriceEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string ComboName {get ;}
    public double ComboPrice {get ;}
    public UpdateComboPriceEvent(string comboName , double comboPrice)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ComboName = comboName;
        ComboPrice = comboPrice;
    }
}