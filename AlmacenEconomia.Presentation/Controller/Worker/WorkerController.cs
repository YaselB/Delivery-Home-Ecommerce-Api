using AlmacenEconomia.Application.Command.Code.CreateOrUpdateCommand;
using AlmacenEconomia.Application.Command.Code.MatchCodeByEmail;
using AlmacenEconomia.Application.Command.Worker.AddPermissions;
using AlmacenEconomia.Application.Command.Worker.Create;
using AlmacenEconomia.Application.Command.Worker.Delete;
using AlmacenEconomia.Application.Command.Worker.RevokePermissions;
using AlmacenEconomia.Application.Command.Worker.UpdateJob;
using AlmacenEconomia.Application.Command.Worker.UpdatePassword;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.Worker.Dto;
using AlmacenEconomia.Application.Query.Worker.GetAll;
using AlmacenEconomia.Application.Query.Worker.GetAllJobs;
using AlmacenEconomia.Application.Query.Worker.GetAllPermissions;
using AlmacenEconomia.Application.Query.Worker.GetById;
using AlmacenEconomia.Application.Query.Worker.Login;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Worker;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Worker;
[ApiController()]
[Route("api/worker")]
public class WorkerController : GenericController<WorkerEntity, CreateWorkerEntityCommand, UpdateWorkerJobCommand, WorkerResultDto>
{
    private readonly IMediator mediator;
    public WorkerController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateWorkerPermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateWorkerEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code ,new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPost("getCode")]
    public async Task<IActionResult> GetCode(CreateOrUpdateCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPost("MatchCode")]
    public async Task<IActionResult> MatchCode(MatchCodeByEmailCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPatch("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(UpdateWorkerPasswordCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , result.error.Message);
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateWorkerJobPermission)]
    [HttpPatch("UpdateJob")]
    public override async Task<IActionResult> Update(UpdateWorkerJobCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.AddWorkerPermission)]
    [HttpPatch("addPermission")]
    public async Task<IActionResult> AddPermission(AddWorkerPermissionsCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.RevokeWorkerPermission)]
    [HttpPatch("revokePermission")]
    public async Task<IActionResult> RevokePermission(RevokeWorkerPermissionsCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command ,cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , result.error.Message);
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteWorkerPermission)]
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new  DeleteWorkerEntityCommand
        {
            Id = id
        };
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {  error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyWorkerPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetWorkerEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllWorkersPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllWorkerEntityQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , result.error.Message);
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllPermissions)]
    [HttpGet("getAllPermissions")]
    public async Task<ActionResult> GetAllPermissions( CancellationToken cancellationToken)
    {
        var query = new GetAllPermissionsQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginWorkerEntityQuery query , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllJobsPermission)]
    [HttpGet("GetAlljobs")]
    public async Task<IActionResult> GetAllJobs(CancellationToken cancellationToken)
    {
        var query = new GetAllJobsQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);

    }
}