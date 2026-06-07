using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Product.UpdatePrice;

public class UpdateProductPriceCommandHandler : UpdateGenericEntityCommandHandler<ProductEntity, UpdateProductPriceCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    public UpdateProductPriceCommandHandler(IProductRepository generic, IMapper mapper , ILogger<ProductEntity> logger) : base(generic, mapper)
    {
        productRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id, cancellationToken);
        if(product == null)
        {
            logger.LogWarning("El producto con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        product.UpdatePrice(request.Price);
        await productRepository.UpdateAsync(product ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}