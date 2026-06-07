using System.ComponentModel.DataAnnotations;
using AlmacenEconomia.Application.Command.Combo.Create;
using AlmacenEconomia.Application.Command.Combo.Delete;
using AlmacenEconomia.Application.Command.Combo.UpdateListProducts;
using AlmacenEconomia.Application.Command.Combo.UpdateName;
using AlmacenEconomia.Application.Command.Combo.UpdatePrice;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.Combo.ResultDto;
using AlmacenEconomia.Application.Query.Combo.GetAll;
using AlmacenEconomia.Application.Query.Combo.GetById;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Combo;
[ApiController]
[Route("api/combo")]

public class ComboController : GenericController<ComboEntity, CreateComboEntityCommand, UpdateComboNameCommand, ComboResultDto>
{
    private readonly IMediator mediator;
    public ComboController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateComboPermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateComboEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateComboPermission)]
    [HttpPatch("updateName")]
    public override async Task<IActionResult> Update(UpdateComboNameCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateComboPermission)]
    [HttpPatch("updatePrice")]
    public async Task<IActionResult> UpdatePrice(UpdateComboPriceCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateComboPermission)]
    [HttpPatch("updateProductList")]
    public async Task<IActionResult> UpdateProductList(UpdateListProductsCommands command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteComboPermission)]
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteComboEntityCommand
        {
            Id = id
        };
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code ,new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyComboPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetComboEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code, new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetAllComboPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllComboEntityQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    
}