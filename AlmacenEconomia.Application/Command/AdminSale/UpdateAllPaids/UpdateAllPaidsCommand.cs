using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.AdminSale.UpdateAllPaids;
public class UpdateAllPaidsCommand : IRequest<Result<Unit>>
{
   public required string AdminId {get ; set ;} = string.Empty; 
}