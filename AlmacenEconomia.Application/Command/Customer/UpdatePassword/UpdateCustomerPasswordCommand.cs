using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.Customer.UpdatePassword;
public class UpdateCustomerPasswordCommand : IRequest<Result<Unit>>
{
    public required string Email {get ; set ; }
    public required string NewPassword {get ; set ;}
}