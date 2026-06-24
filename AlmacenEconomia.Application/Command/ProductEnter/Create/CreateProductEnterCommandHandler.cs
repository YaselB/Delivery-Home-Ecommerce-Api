using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.ProductEnter.Create;

public class CreateProductEnterCommandHandler : CreateGenericEntityCommandHandler<ProductEnterEntity, CreateProductEnterCommand>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEnterEntity> logger;
    public CreateProductEnterCommandHandler(IProductEnterRepository repository, IMapper mapper , IProductRepository product , ILogger<ProductEnterEntity> logger) : base(repository, mapper)
    {
        productEnterRepository = repository;
        productRepository = product;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(CreateProductEnterCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("El producto con id: "+request.ProductId+" no esta registrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        var productEnter = await productEnterRepository.GetByCode(request.Code ,request.ProductId , cancellationToken);
        if(productEnter != null)
        {
            logger.LogWarning("La entrada con codigo: "+request.Code+" esta registrada");
            return Result<Unit>.Failure(new CodeEnterRegisteredError());
        }
        var newQuantity = product.Quantity + request.Quantity;
        var TotalPrice = request.Quantity * request.PriceCup;
        var priceUsd = Math.Round(TotalPrice / request.PriceUsd , 2);
        product.UpdateQuantity(newQuantity);
        var newProductEnter = ProductEnterEntity.Create(request.Code ,request.Quantity , request.PriceCup , priceUsd , request.ProductId ,TotalPrice);
        await productRepository.UpdateAsync(product, cancellationToken);
        await productEnterRepository.AddAsync(newProductEnter, cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}