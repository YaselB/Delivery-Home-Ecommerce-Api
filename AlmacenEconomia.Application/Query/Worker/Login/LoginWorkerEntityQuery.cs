using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Query.Worker.Login;

public class LoginWorkerEntityQuery : IRequest<Result<string?>>
{
    public required string Email { get ; set ;}
    public required string Password {get ; set ;}
}