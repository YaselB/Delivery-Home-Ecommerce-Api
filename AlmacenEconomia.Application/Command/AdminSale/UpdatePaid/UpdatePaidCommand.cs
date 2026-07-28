using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;


namespace AlmacenEconomia.Application.Command.AdminSale.UpdatePaid;
public class UpdatePaidCommand : IRequest<Result<Unit>>
{
    public required string AdminId {get ; set ;}
    public required List<string> SalesId { get ; set ;}
}
