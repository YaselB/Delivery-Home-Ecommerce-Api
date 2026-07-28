using AlmacenEconomia.Application.Command.AdminDebt.CleanUpOldRecords;
using AlmacenEconomia.Application.Command.AdminDebt.Create;
using AlmacenEconomia.Application.Command.AdminDebt.UpdateAllPaids;
using AlmacenEconomia.Application.Command.AdminDebt.UpdatePaid;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Query.AdminDebt.GetAll;
using AlmacenEconomia.Application.Query.AdminDebt.GetByAdminId;
using AlmacenEconomia.Application.Query.AdminDebt.GetById;
using AlmacenEconomia.Domain.Common.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.AdminDebt;

[ApiController]
[Route("api/adminDebt")]
public class AdminDebtController : ControllerBase
{
    private readonly IMediator mediator;
    public AdminDebtController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateAdminDebtPermission)]
    [HttpPost()]
    public async Task<IActionResult> Create(CreateAdminDebtCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { erorr = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateAdminDebtPermission)]
    [HttpPatch()]
    public async Task<IActionResult> Update(UpdateAdminDebtPaidCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateAdminDebtPermission)]
    [HttpPatch("updateAllPaids")]
    public async Task<IActionResult> UpdateAllPaids(UpdateAllAdminDebtPaidsCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteAdminDebtPermission)]
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
    [RequiredPermission(Permissions.GetOnlyAdminDebtPermission)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
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
    [RequiredPermission(Permissions.GetAllAdminDebtPermission)]
    [HttpGet()]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAdminDebtQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code ,new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAdminDebtByAdminIdPermission)]
    [HttpGet("debtByAdmin/{id}")]
    public async Task<ActionResult> GetByAdminId(string id , CancellationToken cancellationToken)
    {
        var query = new GetByAdminIdQuery
        {
            AdminId = id
        };
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error = result.error .Message});
        }
        return Ok(result.Value);
    }
    
}