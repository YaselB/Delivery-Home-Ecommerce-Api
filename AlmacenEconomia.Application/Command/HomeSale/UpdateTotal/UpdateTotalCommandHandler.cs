using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.HomeSale.DetailsDto;
using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AlmacenEconomia.Domain.Entity.HomeSaleDetails;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.HomeSale.UpdateTotal;

public class UpdateTotalCommandHandler : UpdateGenericEntityCommandHandler<HomeSaleEntity, UpdateTotalCommand>
{
    private readonly IHomeSaleRepository homeSaleRepository;
    private readonly ILogger<HomeSaleEntity> logger;
    private readonly IProductRepository productRepository;
    private readonly IProductEnterRepository productEnterRepository;
    public UpdateTotalCommandHandler(IHomeSaleRepository generic, IMapper mapper, ILogger<HomeSaleEntity> logger, IProductRepository product, IProductEnterRepository productEnter) : base(generic, mapper)
    {
        homeSaleRepository = generic;
        this.logger = logger;
        productRepository = product;
        productEnterRepository = productEnter;
    }
    public override async Task<Result<Unit>> Handle(UpdateTotalCommand request, CancellationToken cancellationToken)
    {
        double newTotal = 0;
        var sale = await homeSaleRepository.GetById(request.Id, cancellationToken);
        if (sale == null)
        {
            logger.LogWarning("La salida para la casa con id: " + request.Id + " no esta registrado");
            return Result<Unit>.Failure(new HomeSaleNotFoundError());
        }
        var ids = request.CreateHomes.Select(p => p.ProductId).Distinct().ToList();
        var idsExistentes = sale.HomeSaleDetailsEntities.Select(p => p.ProductId).Distinct().ToList();
        var union = ids.Union(idsExistentes).Distinct().ToList();
        var products = await productRepository.GetByIds(union, cancellationToken);
        var count = products.Count(p => ids.Contains(p.Id));
        if (count != ids.Count())
        {
            logger.LogWarning("algunos productos no estan disponibles en este momento");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        foreach (var i in sale.HomeSaleDetailsEntities.ToList())
        {
            var prod = request.CreateHomes.Where(p => p.ProductId == i.ProductId).Select(p => p.Quantity).FirstOrDefault();
            if (prod > i.Quantity)
            {
                var diference = prod - i.Quantity;
                var product = products.FirstOrDefault(p => p.Id == i.ProductId);
                if (product != null && product.Quantity < diference)
                {
                    logger.LogWarning("El producto: " + product.Name + " no tiene la cantidad suficiente para esta operacion");
                    return Result<Unit>.Failure(new ProductsInListNotAvailableStockError("El producto: " + product.Name + " no tiene la cantidad suficiente para esta operacion"));
                }
            }
        }
        var newIds = sale.HomeSaleDetailsEntities.Select(p => p.ProductId).ToList();
        var newProducts = request.CreateHomes.Where(p => !newIds.Contains(p.ProductId)).ToList();
        foreach(var i in newProducts)
        {
            var prod = products.FirstOrDefault(p => p.Id == i.ProductId);
            if( prod != null && prod.Quantity < i.Quantity)
            {
                logger.LogWarning("El producto con id: "+i.ProductId+" no tiene la cantidad suficiente para esta operación");
                return Result<Unit>.Failure(new ProductsInListNotAvailableStockError("El producto: "+prod.Name+" no tiene la cantidad suficiente para realizar dicho cambio"));
            }
        }
        var productEnter = await productEnterRepository.GetByIdsProducts(union , cancellationToken);
        var groupByProductId = productEnter.GroupBy(p => p.ProductId).ToList();
        foreach (var i in sale.HomeSaleDetailsEntities.ToList())
        {
            var product = request.CreateHomes.FirstOrDefault(p => p.ProductId == i.ProductId);
            var prod = products.FirstOrDefault(p => p.Id == i.ProductId);
            var productEnters = groupByProductId.FirstOrDefault(g => g.Key == i.ProductId);
            var expense = i.Expense;
            if (prod != null && product != null && productEnters != null)
            {
                logger.LogWarning("Producto en la lista HomeSale");
                if (product.Quantity > i.Quantity)
                {
                    logger.LogWarning("La cantidad a entrar es mayor");
                    var diference = product.Quantity - i.Quantity;
                    prod.UpdateQuantity(prod.Quantity - diference);
                    var ordered = productEnters.Where(p => p.Quantity > 0).OrderBy(p => p.CreatedAt);
                    foreach(var j in ordered)
                    {
                        if(j.Quantity >= diference)
                        {
                            j.UpdateQuantity(j.Quantity - diference);
                            await productEnterRepository.UpdateAsync(j , cancellationToken);
                            expense += Math.Round(diference * j.PriceCUP , 2);
                            newTotal += expense;
                            break;
                        }
                        else
                        {
                            diference -= j.Quantity;
                            expense += Math.Round(j.Quantity * j.PriceCUP ,2);
                            newTotal += expense;
                            j.UpdateQuantity(0);
                            await productEnterRepository.UpdateAsync(j , cancellationToken);
                        }
                    }
                    await productRepository.UpdateAsync(prod, cancellationToken);
                }
                if (product.Quantity < i.Quantity)
                {
                    logger.LogWarning("La cantidad a entrar es menor");
                    var diference = i.Quantity - product.Quantity;
                    prod.UpdateQuantity(prod.Quantity + diference);
                    var enter = productEnter.FirstOrDefault(p => p.Quantity > 0);
                    if(enter != null)
                    {
                        logger.LogWarning("La cantidad de la entrada es de :"+enter.Quantity);
                        logger.LogWarning("La cantidad a sumar es : "+diference);
                        enter.UpdateQuantity(enter.Quantity + diference);
                        logger.LogWarning("La nueva cantidad de la entrada es : "+enter.Quantity);
                        await productEnterRepository.UpdateAsync(enter , cancellationToken);
                        expense -= Math.Round(diference * enter.PriceCUP , 2);
                        newTotal += expense;
                    }
                    await productRepository.UpdateAsync(prod , cancellationToken);
                }
                i.UpdateQuantity(product.Quantity);
                i.UpdateExpense(expense);
            }
            if(product == null && prod != null && productEnters != null)
            {
                logger.LogWarning("Producto que no está en la lista HomeSale");
                prod.UpdateQuantity(prod.Quantity + i.Quantity);
                var enter = productEnters.First();
                logger.LogWarning(enter.PriceCUP.ToString());
                if(enter != null)
                {
                    enter.UpdateQuantity(enter.Quantity +i.Quantity);
                    await productEnterRepository.UpdateAsync(enter , cancellationToken);
                }
                sale.HomeSaleDetailsEntities.Remove(i);
                await productRepository.UpdateAsync(prod , cancellationToken);
            } 
        }
        logger.LogWarning("Total antes de agregar: "+newTotal);
        var newDetails = new List<HomeSaleDetailsEntity>();
        foreach(var i in newProducts)
        {
            double expence = 0;
            var prod = products.FirstOrDefault(p => p.Id == i.ProductId);
            var enters = groupByProductId.FirstOrDefault(g => g.Key == i.ProductId);
            if(prod != null && enters != null)
            {
                var ordered = enters.Where(p => p.Quantity > 0).OrderBy(p => p.CreatedAt);
                foreach(var j in ordered)
                {
                    
                    if(j.Quantity > i.Quantity && i.Quantity > 0)
                    {
                        expence += Math.Round(i.Quantity * j.PriceCUP , 2);
                        newTotal += expence;
                        j.UpdateQuantity(Math.Round(j.Quantity - i.Quantity , 2));
                        await productEnterRepository.UpdateAsync(j);
                    }
                    if(j.Quantity < i.Quantity)
                    {
                        expence += Math.Round(j.Quantity * j.PriceCUP , 2);
                        newTotal += expence;
                        j.UpdateQuantity(0);
                        i.Quantity = Math.Round(i.Quantity - j.Quantity ,2);
                        await productEnterRepository.UpdateAsync(j);
                    }
                }
                var newQuantity = Math.Round(prod.Quantity - i.Quantity , 2);
                var newDetail = HomeSaleDetailsEntity.Create(sale.Id ,prod.Id ,i.Quantity ,expence);
                newDetails.Add(newDetail);
                prod.UpdateQuantity(newQuantity);
                await productRepository.UpdateAsync(prod);
            }
        }
        logger.LogWarning("Total despues de agregar: "+newTotal);
        sale.HomeSaleDetailsEntities.AddRange(newDetails);
        newTotal = Math.Round(sale.HomeSaleDetailsEntities.Sum(p => p.Expense) , 2);
        sale.UpdateTotal(newTotal);
        await homeSaleRepository.UpdateAsync(sale , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}