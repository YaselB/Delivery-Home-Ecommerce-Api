using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Domain.Events.ProductEnter.Create;
using AlmacenEconomia.Domain.Events.ProductEnter.UpdateCode;
using AlmacenEconomia.Domain.Events.ProductEnter.UpdatePriceCup;
using AlmacenEconomia.Domain.Events.ProductEnter.UpdateQuantity;

namespace AlmacenEconomia.Domain.Entity.ProductEnter;
public class ProductEnterEntity : GenericEntity<ProductEnterEntity>
{
    public string Code {get ; set ;} = string.Empty;
    public double Quantity {get ; set ;}
    public double PriceCUP {get ; set ;}
    public double PriceUSD {get ; set ;}
    public string ProductId {get ; set ;} = string.Empty;
    public ProductEntity? ProductEntity {get ; set ;}
    public static ProductEnterEntity Create(string code , double quantity , double priceCup , double priceUsd , string productId)
    {
        var productEnter = new ProductEnterEntity
        {
            Code = code,
            Quantity = quantity,
            PriceCUP = priceCup,
            PriceUSD = priceUsd,
            ProductId = productId
        };
        var CreateProductEnterDomainEvent = new CreateProductEnterEntityEvent(productEnter.Id , productEnter.Code);
        productEnter.AddDomainEvent(CreateProductEnterDomainEvent);
        return productEnter;
    }
    public void UpdateCode( string code)
    {
        Code = code;
        UpdatedAt = DateTime.UtcNow;
        var updateProductEnterCodeDomainEvent = new UpdateProductEnterCode(Id , Code);
        AddDomainEvent(updateProductEnterCodeDomainEvent);
    }
    public void UpdateQuantity(double quantity)
    {
        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
        var UpdateQuantityDomainEvent = new UpdateProductEnterQuantityEvent(Id , Quantity);
        AddDomainEvent(UpdateQuantityDomainEvent);
    }
    public void UpdatePriceCup (double priceCup , double priceUsd)
    {
        PriceCUP = priceCup;
        PriceUSD = priceUsd;
        UpdatedAt = DateTime.UtcNow;
        var UpdatePriceCupDomainEvent = new UpdateEnterPriceCupEvent(PriceUSD , PriceCUP);
        AddDomainEvent(UpdatePriceCupDomainEvent);
    }
}