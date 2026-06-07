using AlmacenEconomia.Application.Command.Product.Create;
using AlmacenEconomia.Application.Command.Product.Delete;
using AlmacenEconomia.Application.Command.Product.UpdatePrice;
using AlmacenEconomia.Application.Command.Product.UpdateUnity;
using AlmacenEconomia.Application.Command.Product.UpdateUrl;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.Product.Dto;
using AlmacenEconomia.Application.Query.Product.GetAll;
using AlmacenEconomia.Application.Query.Product.GetAllSection;
using AlmacenEconomia.Application.Query.Product.GetById;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Product;

[ApiController]
[Route("api/product")]
public class ProductController : GenericController<ProductEntity, CreateProductEntityCommand, UpdateProductPriceCommand, ProductResultDto>
{
    private readonly IMediator mediator;
    public ProductController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateProductPermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateProductEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateProductPermission)]
    [HttpPatch("updatePrice")]
    public override async Task<IActionResult> Update(UpdateProductPriceCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateProductPermission)]
    [HttpPatch("updateUrl")]
    public async Task<IActionResult> UpdateUrl(UpdateProductUrlCommand command , CancellationToken cancellationtoken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateProductPermission)]
    [HttpPatch("updateUnit")]
    public async Task<IActionResult> UpdateUnit(UpdateProductUnityCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteProductPermission)]
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductEntityCommand
        {
            Id = id
        };
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyProductPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetProductEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null){
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllProductsPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllProductEntityQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllSections)]
    [HttpGet("getSections")]
    public async Task<ActionResult> GetAllSections()
    {
        var query = new GetAllSectionQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
}