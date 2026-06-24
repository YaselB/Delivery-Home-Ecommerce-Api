using AlmacenEconomia.Application.Command.Generic.Create;
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

namespace AlmacenEconomia.Application.Command.HomeSale.Create;

public class CreateHomeSaleCommandHandler : CreateGenericEntityCommandHandler<HomeSaleEntity, CreateHomeSaleCommand>
{
    public readonly IHomeSaleRepository homeSaleRepository;
    public readonly IProductEnterRepository productEnterRepository;
    public readonly ILogger<HomeSaleEntity> logger;
    public readonly IProductRepository productRepository;
    public CreateHomeSaleCommandHandler(IHomeSaleRepository repository, IMapper mapper, IProductEnterRepository productEnterRepository, ILogger<HomeSaleEntity> logger, IProductRepository productRepository) : base(repository, mapper)
    {
        homeSaleRepository = repository;
        this.productEnterRepository = productEnterRepository;
        this.logger = logger;
        this.productRepository = productRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateHomeSaleCommand request, CancellationToken cancellationToken)
    {
        var ids = request.HomeSaleDtos.Select(p => p.ProductId).Distinct().ToList();
        var products = await productRepository.ContainsId(ids, cancellationToken);
        if (products != ids.Count())
        {
            logger.LogWarning("Algunos productos que se le iba a dar salida no se encuentran");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        var productsEnter = await productEnterRepository.GetByIdsProducts(ids, cancellationToken);
        var groupEnter = productsEnter.GroupBy(p => p.ProductId);
        foreach (var i in groupEnter)
        {
            var quantity = request.HomeSaleDtos.Where(p => p.ProductId == i.First().ProductId).Select(p => p.Quantity).FirstOrDefault();
            if (quantity > i.First().ProductEntity?.Quantity)
            {
                logger.LogWarning("Algunos productos no cuentan con stock suficiente");
                return Result<Unit>.Failure(new ProductsInListNotAvailableStockError("El producto: " + i.First().ProductEntity?.Name + " no tiene disponibilidad"));
            }
        }
        double total = 0;
        var list = new List<DetailsDto>();
        foreach (var i in groupEnter)
        {
            var orderByCreatedAt = i.OrderBy(p => p.CreatedAt).Where(p => p.Quantity > 0);
            var quantity = request.HomeSaleDtos.Where(p => p.ProductId == orderByCreatedAt.First().ProductId).Select(p => p.Quantity).FirstOrDefault();
            var product = orderByCreatedAt.First().ProductEntity;
            if (product != null)
            {
                var detail = new DetailsDto
                {
                    Expense = 0,
                    ProductId = product.Id,
                    Quantity = quantity
                };
                double expense = 0;
                foreach (var j in orderByCreatedAt)
                {
                    if (quantity <= j.Quantity)
                    {
                        j.Quantity -= quantity;
                        product.UpdateQuantity(product.Quantity -= quantity);
                        expense += Math.Round(j.PriceCUP * quantity, 2);
                        total += expense;
                    }
                    if (quantity > j.Quantity)
                    {
                        quantity -= j.Quantity;
                        product.UpdateQuantity(product.Quantity -= j.Quantity);
                        expense += Math.Round(j.PriceCUP * j.Quantity, 2);
                        j.UpdateQuantity(0);
                    }
                    await productEnterRepository.UpdateAsync(j, cancellationToken);
                }
                detail.Expense = expense;
                list.Add(detail);
                await productRepository.UpdateAsync(product);
            }
        }
        var newHomeSale = HomeSaleEntity.Create(total);
        var details = list.Select(p => HomeSaleDetailsEntity.Create(newHomeSale.Id ,p.ProductId , p.Quantity , p.Expense)).ToList();
        newHomeSale.HomeSaleDetailsEntities.AddRange(details);
        await homeSaleRepository.AddAsync(newHomeSale, cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}