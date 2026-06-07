using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateQuantity;

public class UpdateQuantityCommandHandler : UpdateGenericEntityCommandHandler<ProductEnterEntity, UpdateQuantityCommand>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEnterEntity> logger;
    public UpdateQuantityCommandHandler(IProductEnterRepository generic, IMapper mapper , IProductRepository productRepository , ILogger<ProductEnterEntity> logger) : base(generic, mapper)
    {
        this.productRepository = productRepository;
        productEnterRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
    {
        var enter = await productEnterRepository.GetById(request.Id , cancellationToken);
        if(enter == null)
        {
            logger.LogWarning("No se encuentra la entrada con id: "+request.Id);
            return Result<Unit>.Failure(new ProductEnterNotFoundError());
        }
        var product = await productRepository.GetById(enter.ProductId , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("No se encuentra el producto con ese id: "+enter.ProductId);
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        if(request.Quantity < enter.Quantity)
        {
            var diference = enter.Quantity - request.Quantity;
            var newQuantity = product.Quantity - diference;
            product.UpdateQuantity(newQuantity); 
        }
        if(request.Quantity > enter.Quantity)
        {
            var diference = request.Quantity - enter.Quantity;
            var newQuantity = product.Quantity + diference;
            product.UpdateQuantity(newQuantity);
        }
        Console.WriteLine(product.Quantity);
        enter.UpdateQuantity(request.Quantity);
        await productRepository.UpdateAsync(product , cancellationToken);
        await productEnterRepository.UpdateAsync(enter , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}