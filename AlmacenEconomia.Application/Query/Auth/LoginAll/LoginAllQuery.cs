using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Query.Auth.LoginAll;
public class LoginAllQuery : IRequest<Result<string?>>
{
    public required string Email {get ; set ;}
}