using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminSale.DetailsDto;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSaleDetails;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminSale.Create;

public class CreateAdminSaleEntityCommandHandler : CreateGenericEntityCommandHandler<AdminSaleEntity, CreateAdminSaleEntityCommand>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    private readonly IProductRepository productRepository;
    private readonly IProductEnterRepository productEnterRepository;
    private readonly ILogger<AdminSaleEntity> logger;
    private readonly IAdminRepository adminRepository;
    public CreateAdminSaleEntityCommandHandler(IAdminSaleRepository repository, IMapper mapper, IProductRepository productRepository, IProductEnterRepository productEnterRepository, ILogger<AdminSaleEntity> logger, IAdminRepository admin) : base(repository, mapper)
    {
        adminSaleRepository = repository;
        this.productRepository = productRepository;
        this.productEnterRepository = productEnterRepository;
        this.logger = logger;
        adminRepository = admin;
    }
    public override async Task<Result<Unit>> Handle(CreateAdminSaleEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(request.AdminId, cancellationToken);
        if (admin == null)
        {
            logger.LogWarning("No se puede crear una salida para una admin que no exite: " + request.AdminId);
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var ids = request.CreateAdminSaleDtos.Select(p => p.ProductId).Distinct().ToList();
        var products = await productRepository.GetByIds(ids, cancellationToken);
        if (products.Count != ids.Count)
        {
            logger.LogWarning("Se ha intentado crear una salida de productos ,con algunos que no existen");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        foreach (var i in request.CreateAdminSaleDtos.ToList())
        {
            var product = products.FirstOrDefault(p => p.Id == i.ProductId);
            if (product != null && i.Quantity > product.Quantity)
            {
                logger.LogWarning("En la salida para un admin , el producto con id: " + i.ProductId + " no cuenta con el stock suficiente ");
                return Result<Unit>.Failure(new ProductsInListNotAvailableStockError("El producto: " + product.Name + " no tiene la cantidad suficiente para permitir la salida"));
            }
        }
        var entersByProducts = await productEnterRepository.GetByIdsProducts(ids, cancellationToken);
        var entersGroup = entersByProducts.GroupBy(p => p.ProductId);
        var detailsList = new List<DetailsDto>();
        foreach (var i in entersGroup)
        {
            var orderedByCreated = i.OrderBy(p => p.CreatedAt).Where(p => p.Quantity > 0);
            var quantity = request.CreateAdminSaleDtos.Where(p => p.ProductId == orderedByCreated.First().ProductId).Select(p => p.Quantity).FirstOrDefault();
            var product = orderedByCreated.First().ProductEntity;
            if (product != null)
            {
                var detail = new DetailsDto
                {
                    Expense = 0,
                    ProductId = product.Id,
                    Quantity = quantity
                };
                foreach (var j in orderedByCreated)
                {
                    if (quantity <= j.Quantity)
                    {
                        j.UpdateQuantity(Math.Round(j.Quantity - quantity, 2));
                        product.UpdateQuantity(Math.Round(product.Quantity - quantity));
                        detail.Expense += Math.Round(quantity * j.PriceCUP, 2);
                        break;
                    }
                    if (quantity > j.Quantity)
                    {
                        quantity = Math.Round(quantity - j.Quantity , 2);
                        product.UpdateQuantity(Math.Round(product.Quantity - j.Quantity , 2));
                        detail.Expense += Math.Round(j.PriceCUP * j.Quantity , 2);
                        j.UpdateQuantity(0);
                    }
                    await productEnterRepository.UpdateAsync(j , cancellationToken);
                }
                detailsList.Add(detail);
                await productRepository.UpdateAsync(product , cancellationToken);
            }
        }
        var total = detailsList.Sum(p => p.Expense);
        var newSale = AdminSaleEntity.Create(total , admin.Id);
        var details = detailsList.Select(p => AdminSaleDetailsEntity.Create(p.ProductId ,newSale.Id, p.Expense , p.Quantity)).ToList();
        newSale.AdminSaleDetailsEntities.AddRange(details);
        await adminSaleRepository.AddAsync(newSale , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}