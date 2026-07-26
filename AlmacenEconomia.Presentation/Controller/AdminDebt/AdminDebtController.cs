using AlmacenEconomia.Application.Command.AdminDebt.CleanUpOldRecords;
using AlmacenEconomia.Application.Command.AdminDebt.Create;
using AlmacenEconomia.Application.Command.AdminDebt.UpdateAllPaids;
using AlmacenEconomia.Application.Command.AdminDebt.UpdatePaid;
using AlmacenEconomia.Application.Common.ResultWithoutT;
using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using AlmacenEconomia.Application.Query.AdminDebt.GetAll;
using AlmacenEconomia.Application.Query.AdminDebt.GetById;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.AdminDebt;

[ApiController]
[Route("api/adminDebt")]
public class AdminDebtController : GenericController<AdminDebtEntity, CreateAdminDebtCommand, UpdatePaidCommand, AdminDebtDto>
{
    private readonly IMediator mediator;
    public AdminDebtController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateAdminDebtCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { erorr = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPatch()]
    public override async Task<IActionResult> Update(UpdatePaidCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPatch("updateAllPaids")]
    public async Task<IActionResult> UpdateAllPaids(UpdateAllPaidsCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpDelete()]
    public async Task<IActionResult> DeleteOld(CleanupOldRecordsCommand command , CancellationToken cancellationToken)
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
        var query = new GetAdminDebtByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query ,cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAdminDebtQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code ,new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpGet("debtByAdmin/{id}")]
    public async Task<ActionResult> GetByAdminId(string id , CancellationToken cancellationToken)
    {
        var query = new GetAdminDebtByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error = result.error .Message});
        }
        return Ok(result.Value);
    }
    
}