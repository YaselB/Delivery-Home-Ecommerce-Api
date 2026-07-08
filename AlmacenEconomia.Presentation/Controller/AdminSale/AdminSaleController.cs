using AlmacenEconomia.Application.Command.AdminSale.CleanupOldRecordsCommand;
using AlmacenEconomia.Application.Command.AdminSale.Create;
using AlmacenEconomia.Application.Command.AdminSale.UpdateTotal;
using AlmacenEconomia.Application.Features.AdminSale.Dto;
using AlmacenEconomia.Application.Query.AdminSale.GetAll;
using AlmacenEconomia.Application.Query.AdminSale.GetById;
using AlmacenEconomia.Application.Query.AdminSale.GetByProductId;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.AdminSale;

[ApiController]
[Route("api/adminSale")]
public class AdminSaleController : GenericController<AdminSaleEntity, CreateAdminSaleEntityCommand, UpdateAdminSaleTotalCommand, AdminSaleResultDto>
{
    private readonly IMediator mediator;
    public AdminSaleController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateAdminSaleEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPatch()]
    public override async Task<IActionResult> Update(UpdateAdminSaleTotalCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpDelete()]
    public async Task<IActionResult> DeleteOldSales(CancellationToken cancellationToken)
    {
        var command = new CleanupOldRecordsCommand();
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetAdminSaleByIdQuery
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
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAdminSaleQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpGet("/{productId}")]
    public async Task<ActionResult> GetByProductId(string productId , CancellationToken cancellationToken)
    {
        var query = new GetAdminSaleByProductIdQuery
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
