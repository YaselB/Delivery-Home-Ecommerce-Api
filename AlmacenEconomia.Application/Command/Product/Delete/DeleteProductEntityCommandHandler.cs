using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Domain.Events.Product.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Product.Delete;

public class DeleteProductEntityCommandHandler : DeleteGenericEntityCommandHandler<ProductEntity, DeleteProductEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    public DeleteProductEntityCommandHandler(IProductRepository genericRepository , ILogger<ProductEntity> logger) : base(genericRepository)
    {
        productRepository = genericRepository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(DeleteProductEntityCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id,  cancellationToken);
        if(product == null)
        {
            logger.LogWarning("El producto con id: "+request.Id+" no se encuentra");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        var deleteProductDomainEvent = new DeleteProductEntityEvent(product.Name , product.Id);
        product.AddDomainEvent(deleteProductDomainEvent);
        await productRepository.DeleteAsync(product , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}