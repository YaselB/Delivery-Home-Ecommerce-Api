using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Command.ProductEnter.Create;
public class CreateProductEnterCommand : CreateGenericEntityCommand<ProductEnterEntity>
{
    public string Code {get ; set ;} = string.Empty;
    public double Quantity {get ; set ;}
    public double PriceCup {get ; set ;}
    public double PriceUsd {get ; set ;}
    public string ProductId {get ; set ;} = string.Empty;
}