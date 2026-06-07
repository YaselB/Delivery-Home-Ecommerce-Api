using AlmacenEconomia.Application.Command.Offer.Create;
using AlmacenEconomia.Application.Command.Offer.Delete;
using AlmacenEconomia.Application.Command.Offer.UpdateName;
using AlmacenEconomia.Application.Command.Offer.UpdatePrice;
using AlmacenEconomia.Application.Command.Offer.UpdateProductList;
using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Features.Offer.OfferResult;
using AlmacenEconomia.Application.Query.Offer.GetAll;
using AlmacenEconomia.Application.Query.Offer.GetById;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Offer;
[ApiController]
[Route("api/offer")]
public class OfferController : GenericController<OfferEntity, CreateOfferEntityCommand, UpdateOfferNameCommand, OfferResultDto>
{
    private readonly IMediator mediator;
    public OfferController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.CreateOfferPermission)]
    [HttpPost()]
    public override async Task<IActionResult> Create(CreateOfferEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateOfferPermission)]
    [HttpPatch("updateName")]
    public override async Task<IActionResult> Update(UpdateOfferNameCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateOfferPermission)]
    [HttpPatch("updatePrice")]
    public async Task<IActionResult> UpdatePrice(UpdateOfferPriceCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.UpdateOfferPermission)]
    [HttpPatch("updateProductList")]
    public async Task<IActionResult> UpdateProductList(UpdateProductListCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.DeleteOfferPermission)]
    [HttpDelete()]
    public override async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteOfferEntityCommand
        {
            Id = id
        };
        var result = await mediator.Send(command , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [RequiredPermission(Permissions.GetOnlyOfferPermission)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetOfferEntityByIdQuery
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
    [RequiredPermission(Permissions.GetAllOfferPermission)]
    [HttpGet()]
    public override async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllOfferEntityQuery();
        var result = await mediator.Send(query , cancellationToken);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
}