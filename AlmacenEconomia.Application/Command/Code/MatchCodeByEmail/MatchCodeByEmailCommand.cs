using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.Code.MatchCodeByEmail;
public class MatchCodeByEmailCommand : IRequest<Result<Unit>>
{
    public required string Email {get ; set ;}
    public required string Code {get ; set ;}
}