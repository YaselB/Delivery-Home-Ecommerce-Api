using AlmacenEconomia.Application.Command.ProductEnter.Create;
using AlmacenEconomia.Application.Command.ProductEnter.UpdateCode;
using AlmacenEconomia.Application.Command.ProductEnter.UpdatePriceCup;
using AlmacenEconomia.Application.Command.ProductEnter.UpdateQuantity;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.ProductEnter.Dto;
using AlmacenEconomia.Application.Query.ProductEnter.GetAll;
using AlmacenEconomia.Application.Query.ProductEnter.GetbyId;
using AlmacenEconomia.Application.Query.ProductEnter.GetByIdProduct;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.ProductEnter;
[ApiController]
[Route("api/productEnter")]
public class ProductEnterController : GenericController<ProductEnterEntity, CreateProductEnterCommand, UpdateCodeCommand, ProductEnterResultDto>
{
    private readonly IMediator mediator;
    public ProductEnterController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateProductEnterPermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateProductEnterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateProductEnterPermission)]
    [HttpPatch()]
    public override async Task<IActionResult> Update(UpdateCodeCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateProductEnterPermission)]
    [HttpPatch("updateQuantity")]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateProductEnterPermission)]
    [HttpPatch("updatePrice")]
    public async Task<IActionResult> UpdatePrice(UpdatePriceCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send( command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyProductEnterPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetProductEnterByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query ,cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllProductEnterPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllProductEnterQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetEnterByProductIdPermission)]
    [HttpGet("product/{productId}")]
    public async Task<ActionResult> GetByProducts(string productId ,CancellationToken cancellationToken)
    {
        var query = new GetByIdProductQuery
        {
            ProductId = productId
        };
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    
}