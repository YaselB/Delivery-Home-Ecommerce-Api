using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.ProductSections;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Product.Create;

public class CreateProductEntityCommandHandler : CreateGenericEntityCommandHandler<ProductEntity, CreateProductEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    public CreateProductEntityCommandHandler(IProductRepository repository, IMapper mapper , ILogger<ProductEntity> logger) : base(repository, mapper)
    {
        productRepository = repository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(CreateProductEntityCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByName(request.Name , cancellationToken);
        if(product != null)
        {
            logger.LogWarning("El producto con nombre: "+request.Name+" ya esta registrado");
            return Result<Unit>.Failure(new ProductRegisteredError());
        }
        var list = ProductSections.AllSections.ToList();
        if (!list.Contains(request.Section))
        {
            logger.LogWarning("Se ha intentado crear un producto con una sección incorrecta: "+request.Section);
            return Result<Unit>.Failure(new ProductSectionNotFoundError());
        }
        var newProduct = ProductEntity.Create(request.Name ,request.Section , request.Url ,request.Price , request.Unity);
        await productRepository.AddAsync(newProduct , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}