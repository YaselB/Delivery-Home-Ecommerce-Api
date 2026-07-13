using AlmacenEconomia.Application.Command.HomeSale.Create;
using AlmacenEconomia.Application.Command.HomeSale.UpdateTotal;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Query.HomeSale.GetAll;
using AlmacenEconomia.Application.Query.HomeSale.GetById;
using AlmacenEconomia.Application.Query.HomeSale.GetProductIdQuery;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Application.Controller.HomeSaleController;
[ApiController]
[Route("api/homeSale")]
public class HomeSaleController : GenericController<HomeSaleEntity, CreateHomeSaleCommand, UpdateTotalCommand, HomeSaleResultDto>
{
    private readonly IMediator mediator;
    public HomeSaleController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateHomeSalePermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateHomeSaleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateHomeSalePermission)]
    [HttpPatch()]
    public override async Task<IActionResult> Update(UpdateTotalCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyHomeSalePermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetHomeSaleEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllHomeSalePermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllHomeSaleEntityQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetHomeSaleByProductId)]
    [HttpGet("product/{productId}")]
    public async Task<ActionResult> GetByProductId(string productId , CancellationToken cancellationToken)
    {
        var query = new GetByProductIdQuery
        {
            ProductId = productId
        };
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error = result.error.Message});
        }
        return Ok(result.Value);
    }
}