using AlmacenEconomia.Application.Common.Security;
using AlmacenEconomia.Application.Query.Auth.LoginAll;
using AlmacenEconomia.Domain.Common.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenEconomia.Presentation.Controller.Auth;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [RequiredPermission(Permissions.Auth)]
    [HttpPost()]
    public async Task<IActionResult> LoginALl(LoginAllQuery query ,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.Code , new { error = result.error.Message});
        }
        return Ok(result.Value);
    }
}