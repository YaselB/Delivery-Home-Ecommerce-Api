using AlmacenEconomia.Application.Command.Code.CreateOrUpdateCommand;
using AlmacenEconomia.Application.Command.Code.MatchCodeByEmail;
using AlmacenEconomia.Application.Command.Customer.AddPermissions;
using AlmacenEconomia.Application.Command.Customer.Create;
using AlmacenEconomia.Application.Command.Customer.Delete;
using AlmacenEconomia.Application.Command.Customer.RevokePermissions;
using AlmacenEconomia.Application.Command.Customer.UpdatePassword;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.Customer.Dto;
using AlmacenEconomia.Application.Query.Customer.GetAll;
using AlmacenEconomia.Application.Query.Customer.GetAllPermissions;
using AlmacenEconomia.Application.Query.Customer.GetById;
using AlmacenEconomia.Application.Query.Customer.Login;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Customer;

[ApiController]
[Route("api/customer")]
public class CustomerController : GenericController<CustomerEntity, CreateCustomerEntityCommand, AddPermissionsCustomerCommand, CustomerResultDto>
{
    private readonly IMediator mediator;
    public CustomerController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateCustomerEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [HttpPost("getCode")]
    public async Task<IActionResult> GetCode(CreateOrUpdateCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [HttpPost("matchCode")]
    public async Task<IActionResult> MatchCode(MatchCodeByEmailCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [HttpPatch("updatePassword")]
    public async Task<IActionResult> UpdatePassword(UpdateCustomerPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.AddPermissionToCustomer)]
    [HttpPatch("addPermissions")]
    public override async Task<IActionResult> Update(AddPermissionsCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.RevokePermissionToCustomer)]
    [HttpPatch("RevokePermissions")]
    public async Task<IActionResult> RevokePermissions(RevokePermissionsCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteCustomerPermission)]
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerEntityCommand
        {
            Id = id
        };
        var result = await mediator.Send(command);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyCustomerPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message });
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllCustomersPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCustomerEntityQuery();
        var result = await mediator.Send(query);
        if (result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, result.error.Message);
        }
        return Ok(result.Value);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCustomerEntityQuery query , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllPermissions)]
    [HttpGet("getAllPermissions")]
    public async Task<ActionResult> GetAllPermissions(CancellationToken cancellationToken)
    {
        var query = new GetAllPermissionsQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
}