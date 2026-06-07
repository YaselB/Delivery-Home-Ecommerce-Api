namespace AlmacenEconomia.Application.Features.ProductEnter.Dto;
public class ProductEnterResultDto
{
    public required string Id {get ; set ;}
    public required string Name {get ; set ;}
    public required string Code {get ; set ;}
    public required string Unity {get ; set ;}
    public required double PriceUsd {get ; set ;}
    public required double PriceCup {get ; set ;}
    public required double StockQuantity {get ; set ;}
    public required double Quantity {get ; set ;}
    
}