using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdatePriceCup;
public class UpdatePriceCommand : UpdateGenericEntityCommand<ProductEnterEntity>
{
    public required double PriceCup {get ; set ;}
    public required double PriceUsd {get ; set ;}
}