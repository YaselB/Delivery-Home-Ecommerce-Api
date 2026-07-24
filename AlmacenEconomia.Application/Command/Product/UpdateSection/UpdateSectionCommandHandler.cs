using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.ProductSections;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Product.UpdateSection;

public class UpdateSectionCommandHandler : UpdateGenericEntityCommandHandler<ProductEntity, UpdateSectionCommand>
{
    private readonly ILogger<ProductEntity> logger;
    private readonly IProductRepository productRepository;
    public UpdateSectionCommandHandler(IProductRepository generic, IMapper mapper , ILogger<ProductEntity> logger) : base(generic, mapper)
    {
        this.logger = logger;
        this.productRepository = generic;
    }
    public override async Task<Result<Unit>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("El producto con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        var sections = ProductSections.AllSections.ToList();
        if (!sections.Contains(request.Section))
        {
            logger.LogWarning("La seccion "+request.Section+" no es valida");
            return Result<Unit>.Failure(new SectionNotFoundError());
        }
        product.UpdateSection(request.Section);
        await productRepository.UpdateAsync(product , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}