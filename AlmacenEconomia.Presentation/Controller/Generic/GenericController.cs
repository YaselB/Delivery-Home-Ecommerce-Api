using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Generic;
[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity , TCreateCommand , TUpdateCommand ,TResultDto> : ControllerBase
where TEntity : GenericEntity<TEntity> , new ()
where TCreateCommand : CreateGenericEntityCommand<TEntity> ,new ()
where TUpdateCommand : UpdateGenericEntityCommand<TEntity> 
where TResultDto : class
{
    private readonly IMediator mediator;
    public GenericController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost()]
    public virtual async Task<IActionResult> Create(TCreateCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpPut()]
    public virtual async Task<IActionResult> Update(TUpdateCommand command , CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error =  result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(string id , CancellationToken cancellationToken)
    {
        var command = new DeleteGenericEntityCommand<TEntity>
        {
            Id = id
        };
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new {error = result.error.Message});
        }
        return Ok(result.Value);
    }
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(string id , CancellationToken cancellationToken)
    {
        var query = new GetGenericEntityByIdQuery<TEntity ,TResultDto>
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
    [HttpGet()]
    public virtual async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllGenericEntityQuery<TEntity , TResultDto>();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
    
}
