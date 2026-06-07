using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.ProductEnter.UpdatePriceCup;

public class UpdateEnterPriceCupEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public double NewPriceUsd {get ;}
    public double NewPriceCup {get ;}
    public UpdateEnterPriceCupEvent(double newPriceUsd , double newPriceCup)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        NewPriceUsd = newPriceUsd;
        NewPriceCup = newPriceCup;
    }
}