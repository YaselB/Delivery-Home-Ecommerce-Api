using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.Unity;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Product.UpdateUnity;

public class UpdateProductUnityCommandHandler : UpdateGenericEntityCommandHandler<ProductEntity, UpdateProductUnityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    public UpdateProductUnityCommandHandler(IProductRepository generic, IMapper mapper , ILogger<ProductEntity> logger) : base(generic, mapper)
    {
        productRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateProductUnityCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id , cancellationToken);
        if(product == null)
        {
            logger.LogInformation("El producto con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        var list = Unities.AllUnities.ToList();
        if (!list.Contains(request.Unity))
        {
            logger.LogWarning("La unidad que desea entrar no existe: "+request.Unity);
            return Result<Unit>.Failure(new ProductUnitNotFound());
        }
        if(request.Unity == Unities.Unity)
        {
            logger.LogWarning("Se esta tratando de actualizar de :"+product.Unity+" a: "+request.Unity);
            return Result<Unit>.Failure(new ConvertToUnitError());
        }
        if(product.Unity == Unities.Unity)
        {
            logger.LogWarning("Se esta tratando de actualizar de: "+product.Unity+" a: "+request.Unity);
            return Result<Unit>.Failure(new ConvertSinceUnitError());
        }
        if(product.Unity == request.Unity)
        {
            logger.LogWarning("Se ha intentado actualizar una unidad de medida que era la que estaba registrada para ese producto");
            return Result<Unit>.Failure(new EqualsUnitiesError());
        }
        var newQuantity = Unities.Convert(request.Unity , product.Quantity);
        product.UpdateUnity(request.Unity , newQuantity);
        await productRepository.UpdateAsync(product , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}