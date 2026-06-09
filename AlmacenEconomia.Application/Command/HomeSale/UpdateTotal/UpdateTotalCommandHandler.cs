using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
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
        var sale = await homeSaleRepository.GetById(request.Id, cancellationToken);
        if (sale == null)
        {
            logger.LogWarning("La salida para la casa con id: " + request.Id + " no esta registrado");
            return Result<Unit>.Failure(new HomeSaleNotFoundError());
        }
        var ids = request.CreateHomes.Select(p => p.ProductId).Distinct().ToList();
        var products = await productRepository.GetByIds(ids, cancellationToken);
        if (products.Count != ids.Count())
        {
            logger.LogWarning("algunos productos no estan disponibles en este momento");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        foreach (var i in sale.HomeSaleDetailsEntities.ToList())
        {
            var prod = request.CreateHomes.Where(p => p.ProductId == i.Id).Select(p => p.Quantity).FirstOrDefault();
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
        var productEnter = await productEnterRepository.GetByIdsProducts(ids , cancellationToken);
        var groupByProductId = productEnter.GroupBy(p => p.ProductId).ToList();
        foreach (var i in sale.HomeSaleDetailsEntities.ToList())
        {
            var product = request.CreateHomes.FirstOrDefault(p => p.ProductId == i.ProductId);
            var prod = products.FirstOrDefault(p => p.Id == i.ProductId);
            var productEnters = groupByProductId.FirstOrDefault(g => g.Key == i.ProductId);
            if (prod != null && product != null && productEnters != null)
            {
                if (product.Quantity > i.Quantity)
                {
                    var diference = product.Quantity - i.Quantity;
                    prod.UpdateQuantity(prod.Quantity - diference);
                    var ordered = productEnters.OrderBy(p => p.CreatedAt);
                    foreach(var j in ordered)
                    {
                        if(j.Quantity >= diference)
                        {
                            j.UpdateQuantity(j.Quantity - diference);
                            await productEnterRepository.UpdateAsync(j , cancellationToken);
                            break;
                        }
                        else
                        {
                            diference -= j.Quantity;
                            j.UpdateQuantity(0);
                            await productEnterRepository.UpdateAsync(j , cancellationToken);
                        }
                    }
                    await productRepository.UpdateAsync(prod, cancellationToken);
                }
                if (product.Quantity < i.Quantity)
                {
                    var diference = i.Quantity - product.Quantity;
                    prod.UpdateQuantity(prod.Quantity + diference);
                    var enter = productEnter.First();
                    if(enter != null)
                    {
                        enter.UpdateQuantity(enter.Quantity + diference);
                        await productEnterRepository.UpdateAsync(enter , cancellationToken);
                    }
                    await productRepository.UpdateAsync(prod , cancellationToken);
                }
            }
            if(product == null && prod != null && productEnters != null)
            {
                prod.UpdateQuantity(prod.Quantity + i.Quantity);
                var enter = productEnters.First();
                if(enter != null)
                {
                    enter.UpdateQuantity(enter.Quantity +i.Quantity);
                    await productEnterRepository.UpdateAsync(enter , cancellationToken);
                }
                sale.HomeSaleDetailsEntities.Remove(i);
                await productRepository.UpdateAsync(prod , cancellationToken);
            } 
        }
        var detailsIds = sale.HomeSaleDetailsEntities.Select(p => p.ProductId).ToList();
        var notExists = request.CreateHomes.Where(h => !detailsIds.Contains(h.ProductId)).Select(h => HomeSaleDetailsEntity.Create(sale.Id ,h.ProductId , h.Quantity));
        sale.HomeSaleDetailsEntities.AddRange(notExists);
        sale.UpdateTotal(sale.HomeSaleDetailsEntities.Sum(p => p.Quantity));
        await homeSaleRepository.UpdateAsync(sale , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}