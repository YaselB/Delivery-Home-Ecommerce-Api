using AlmacenEconomia.Application.Command.HomeSale.Create;
using AlmacenEconomia.Application.Command.HomeSale.UpdateTotal;
using AlmacenEconomia.Application.Features.HomeSale.ResultDto;
using AlmacenEconomia.Application.Query.HomeSale.GetAll;
using AlmacenEconomia.Application.Query.HomeSale.GetById;
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
            return Ok(result.Value);
        }
        return Ok(result.Value);
    }
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
}