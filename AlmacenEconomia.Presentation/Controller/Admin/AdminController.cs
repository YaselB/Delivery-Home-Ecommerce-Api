using AlmacenEconomia.Application.Command.Admin.Create;
using AlmacenEconomia.Application.Command.Admin.Delete;
using AlmacenEconomia.Application.Features.Admin.Dto;
using AlmacenEconomia.Application.Query.Admin.GetAll;
using AlmacenEconomia.Application.Query.Admin.GetById;
using AlmacenEconomia.Application.Query.Admin.Login;
using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Application.Command.Admin.AddPermission;
using AlmacenEconomia.Application.Command.Admin.RevokePermission;
using AlmacenEconomia.Application.Command.Code.CreateOrUpdateCommand;
using AlmacenEconomia.Application.Command.Code.MatchCodeByEmail;
using AlmacenEconomia.Application.Query.Admin.GetAllPermissions;
using AlmacenEconomia.Application.Command.Admin.Update;

namespace AlmacenEconomia.Presentation.Controller.Admin;
[ApiController]
[Route("api/admin")]
public class AdminController : GenericController<AdminEntity, CreateAdminEntityCommand, AddPermissionAdminEntity, AdminResultDto>
{
    private readonly IMediator mediator1;
    public AdminController(IMediator mediator) : base(mediator)
    {
        mediator1 = mediator;
    }
    [RequiredPermission(Permissions.CreateAdminPermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateAdminEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code ,new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.AddPermissionToAdmin)]
    [HttpPut("addPermissions")]
    public override async Task<IActionResult> Update(AddPermissionAdminEntity command, CancellationToken cancellationToken)
    {
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.RevokePermissionToAdmin)]
    [HttpPatch("revokePermission")]
    public async Task<IActionResult> RevokePermissions(RevokePermissionsAdminEntityCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new  { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPost("getCode")]
    public async Task<IActionResult> GetCode(CreateOrUpdateCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});   
        }
        return Ok(result.Value);
    }
    [HttpPost("matchCode")]
    public async Task<IActionResult> MatchResult(MatchCodeByEmailCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteAdminPermission)]
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteAdminEntityCommand
        {
            Id = id
        };
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code ,new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyAdminPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetAdminEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator1.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllAdminPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAdminEntityQuery();
        var result = await mediator1.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAdminEntityQuery query , CancellationToken cancellationToken)
    {
        var result = await mediator1.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllPermissions)]
    [HttpGet("getAllPermissions")]
    public async Task<IActionResult> GetAllPermissions(CancellationToken cancellationToken)
    {
        var query = new GetAllPermissionsQuery();
        var result = await mediator1.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPatch("updatePassword")]
    public async Task<IActionResult> UpdatePassword(UpdateAdminEntityCommand command)
    {
        var result = await mediator1.Send(command);
        if(result.IsFailure && result.error != null){
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
}