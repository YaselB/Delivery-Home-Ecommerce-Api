using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSaleDetails;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminSale.UpdateTotal;

public class UpdateAdminSaleTotalCommandHandler : UpdateGenericEntityCommandHandler<AdminSaleEntity, UpdateAdminSaleTotalCommand>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    private readonly IProductRepository productRepository;
    private readonly IProductEnterRepository productEnterRepository;
    private readonly ILogger<AdminSaleEntity> logger;
    public UpdateAdminSaleTotalCommandHandler(IAdminSaleRepository generic, IMapper mapper , IProductEnterRepository productEnterRepository , IProductRepository productRepository, ILogger<AdminSaleEntity> logger) : base(generic, mapper)
    {
        adminSaleRepository = generic;
        this.productRepository = productRepository;
        this.productEnterRepository = productEnterRepository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateAdminSaleTotalCommand request, CancellationToken cancellationToken)
    {
        var adminSale = await adminSaleRepository.GetById(request.Id , cancellationToken);
        if(adminSale == null)
        {
            logger.LogWarning("La salida de admin con id: "+request.Id+" no esta registrada");
            return Result<Unit>.Failure(new AdminSaleNotFoundError());
        }
        var oldList = adminSale.AdminSaleDetailsEntities.Select(p => p.ProductId).Distinct().ToList();
        var newList = request.CreateAdminSaleDtos.Select(p => p.ProductId).Distinct().ToList();
        var union = oldList.Union(newList).Distinct().ToList();
        var productList = await productRepository.GetByIds(union ,cancellationToken);
        if(productList.Count != union.Count)
        {
            logger.LogWarning("Algunos de los productos de la operacion no estan registrados");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        foreach(var i in adminSale.AdminSaleDetailsEntities.ToList())
        {
            var quantity = request.CreateAdminSaleDtos.Where(p => p.ProductId == i.ProductId).Select(p => p.Quantity).FirstOrDefault();
            if(quantity > i.Quantity)
            {
                var productDiference = Math.Round(quantity -i.Quantity , 2);
                var product = productList.FirstOrDefault(p => p.Id == i.ProductId);
                if(product != null && productDiference > product.Quantity)
                {
                    logger.LogWarning("Algunos productos no tienen el stock suficiente para realizar dicho cambio");
                    return Result<Unit>.Failure(new ProductsInListNotAvailableStockError("El producto: "+product.Name+" no cuenta con la cantidad suficiente: "+product.Quantity+" para actualizar la salida"));
                }
            }
        }
        var newIds = adminSale.AdminSaleDetailsEntities.Select(p => p.ProductId).ToList();
        var newProducts = request.CreateAdminSaleDtos.Where(p => !newIds.Contains(p.ProductId)).ToList();
        foreach(var i in newProducts)
        {
            var product = productList.FirstOrDefault(p => p.Id == i.ProductId);
            if(product != null && product.Quantity < i.Quantity)
            {
                logger.LogWarning("Algunos productos no tiene stock disponible ,para realizar dicha operacion");
                return Result<Unit>.Failure(new ProductsInListNotAvailableStockError("El producto: "+product.Name+" no contiene la cantidad necesaria ,para realizar dicha accion "));
            }
        }
        var productEnter = await productEnterRepository.GetByIdsProducts(union , cancellationToken);
        var productsEnterGrouping = productEnter.GroupBy(p => p.ProductId).ToList();
        foreach(var i in adminSale.AdminSaleDetailsEntities.ToList())
        {
            var product = productList.FirstOrDefault(p => p.Id == i.ProductId);
            var updateProduct = request.CreateAdminSaleDtos.FirstOrDefault(p => p.ProductId == i.ProductId);
            var enters = productsEnterGrouping.FirstOrDefault(p => p.Key == i.ProductId);
            var expence = i.Expensive;
            if(product != null && updateProduct != null && enters != null)
            {
                if(updateProduct.Quantity > i.Quantity)
                {
                    var diference = updateProduct.Quantity - i.Quantity;
                    product.UpdateQuantity(Math.Round(product.Quantity - diference , 2));
                    var entersOrdered = enters.Where(p => p.Quantity > 0).OrderBy(p => p.CreatedAt).ToList();
                    foreach(var j in entersOrdered)
                    {
                        if(j.Quantity >= diference)
                        {
                            j.UpdateQuantity(j.Quantity - diference);
                            expence += Math.Round(diference * j.PriceCUP , 2);
                            await productEnterRepository.UpdateAsync(j , cancellationToken);
                            break;
                        }
                        else
                        {
                            diference -= j.Quantity;
                            expence += Math.Round(j.Quantity * j.PriceCUP , 2);
                            j.UpdateQuantity(0);
                            await productEnterRepository.UpdateAsync(j , cancellationToken);
                        }
                    }
                }
                if(updateProduct.Quantity < i.Quantity)
                {
                    var diference = Math.Round(i.Quantity - updateProduct.Quantity);
                    product.UpdateQuantity(Math.Round(product.Quantity + diference , 2));
                    var enter = enters.FirstOrDefault(p => p.Quantity > 0);
                    if(enter != null)
                    {
                        enter.UpdateQuantity(Math.Round(enter.Quantity + diference , 2)); 
                        expence += Math.Round(diference * enter.PriceCUP);
                        await productEnterRepository.UpdateAsync(enter , cancellationToken);
                    }
                }
                i.UpdateQuantity(updateProduct.Quantity);
                await productRepository.UpdateAsync(product , cancellationToken);
            }
            if(product != null && updateProduct == null && enters != null)
            {
                product.UpdateQuantity(Math.Round(product.Quantity + i.Quantity , 2));
                var enter = enters.First();
                if(enter != null)
                {
                    enter.UpdateQuantity(Math.Round(enter.Quantity + i.Quantity));
                    await productEnterRepository.UpdateAsync(enter , cancellationToken);
                }
                adminSale.AdminSaleDetailsEntities.Remove(i);
                await productRepository.UpdateAsync(product , cancellationToken);
            }
        }
        var newDetails = new List<AdminSaleDetailsEntity>();
        foreach(var i in newProducts)
        {
            double expence = 0;
            var quantity = i.Quantity;
            var product = productList.FirstOrDefault(p => p.Id == i.ProductId);
            var enter = productsEnterGrouping.FirstOrDefault(p => p.Key == i.ProductId);
            if(product != null && enter != null)
            {
                var entersOrdered = enter.Where(p => p.Quantity > 0).OrderBy(p => p.CreatedAt);
                foreach(var j in entersOrdered)
                {
                    if(j.Quantity >= quantity && quantity > 0)
                    {
                        j.UpdateQuantity(Math.Round(j.Quantity - quantity , 2));
                        await productEnterRepository.UpdateAsync(j , cancellationToken);
                        expence += Math.Round(quantity * j.PriceCUP);
                        break;
                    }
                    if(j.Quantity < quantity)
                    {
                        quantity = Math.Round(quantity - j.Quantity , 2);
                        expence += Math.Round(j.Quantity * j.PriceCUP , 2);
                        j.UpdateQuantity(0);
                        await productEnterRepository.UpdateAsync(j , cancellationToken);
                    }
                }
                product.UpdateQuantity(Math.Round(product.Quantity - quantity , 2));
                var newDetail = AdminSaleDetailsEntity.Create(product.Id , adminSale.Id , expence , i.Quantity);
                newDetails.Add(newDetail);
                await productRepository.UpdateAsync(product , cancellationToken);
            }
        }
        adminSale.AdminSaleDetailsEntities.AddRange(newDetails);
        var newTotal = Math.Round(adminSale.AdminSaleDetailsEntities.Sum(p => p.Expensive));
        adminSale.UpdateTotal(newTotal);
        await adminSaleRepository.UpdateAsync(adminSale , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}