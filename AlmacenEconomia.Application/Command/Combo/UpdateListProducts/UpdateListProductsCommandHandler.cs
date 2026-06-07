using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Entity.ComboDetails;
using AlmacenEconomia.Domain.Events.Combo.UpdateProductList;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Combo.UpdateListProducts;

public class UpdateListProductsCommandHandler : UpdateGenericEntityCommandHandler<ComboEntity, UpdateListProductsCommands>
{
    private readonly IProductRepository productRepository;
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    public UpdateListProductsCommandHandler(IComboRepository generic, IMapper mapper , IProductRepository product , ILogger<ComboEntity> logger) : base(generic, mapper)
    {
        productRepository = product;
        comboRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateListProductsCommands request, CancellationToken cancellationToken)
{
    var combo = await comboRepository.GetById(request.Id, cancellationToken);
    if (combo == null)
    {
        logger.LogWarning("Se ha intentado actualizar un combo inexistente: " + request.Id);
        return Result<Unit>.Failure(new ComboNotFoundError());
    }

    // Validar que todos los productos enviados existan
    var productosEnRequest = request.CreateComboDtos
        .GroupBy(d => d.ProductId) // Agrupar para manejar duplicados
        .Select(g => new { ProductId = g.Key, Quantity = g.Sum(d => d.Quantity) })
        .ToList();

    var productIds = productosEnRequest.Select(p => p.ProductId).ToList();
    var cantidadProductosValidos = await productRepository.ContainsId(productIds, cancellationToken);
    if (productIds.Count != cantidadProductosValidos)
    {
        logger.LogWarning("Se ha intentado actualizar los productos del combo: " + combo.Name + " pero algunos no existen");
        return Result<Unit>.Failure(new ProductsNotRegisteredError());
    }

    // Actualizar cantidades de los que ya existen
    foreach (var detalle in combo.ComboDetails.ToList()) // ToList() para evitar modificar mientras se itera
    {
        var nuevo = productosEnRequest.FirstOrDefault(p => p.ProductId == detalle.ProductId);
        if (nuevo != null)
        {
            detalle.Quantity = nuevo.Quantity;  // Actualizar cantidad
        }
        else
        {
            combo.ComboDetails.Remove(detalle); // Eliminar los que ya no están
        }
    }

    // Agregar los productos nuevos (que no estaban en el combo)
    var idsExistentes = combo.ComboDetails.Select(cd => cd.ProductId).ToHashSet();
    var nuevosProductos = productosEnRequest
        .Where(p => !idsExistentes.Contains(p.ProductId))
        .Select(p => ComboDetailsEntity.Create(combo.Id, p.ProductId, p.Quantity))
        .ToList();

    combo.ComboDetails.AddRange(nuevosProductos);
    var updateProductListDomainEvent = new UpdateProductListEvent(combo.Id , combo.Name);
    combo.AddDomainEvent(updateProductListDomainEvent);
    await comboRepository.UpdateAsync(combo , cancellationToken);
    return Result<Unit>.Success(Unit.Value);
} 
}