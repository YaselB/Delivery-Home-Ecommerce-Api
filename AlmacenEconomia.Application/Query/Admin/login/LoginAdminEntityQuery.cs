using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Query.Admin.Login;
public class LoginAdminEntityQuery : IRequest<Result<string?>>
{
    public required string Email { get ; set ;}
    public required string Password {get ; set ;}
}