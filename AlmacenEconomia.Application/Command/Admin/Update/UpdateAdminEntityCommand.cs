using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.Admin.Update;
public class UpdateAdminEntityCommand : IRequest<Result<Unit>>
{
    public required string NewPassword {get ; set ;}
    public required string Email { get ; set ;}
}