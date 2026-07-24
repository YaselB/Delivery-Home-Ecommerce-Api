using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using MediatR;

namespace AlmacenEconomia.Application.Command.AdminSale.UpdateAllPaids;

public class UpdateAllPaidsCommandHandler : IRequestHandler<UpdateAllPaidsCommand, Result<Unit>>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    public UpdateAllPaidsCommandHandler(IAdminSaleRepository adminSaleRepository)
    {
        this.adminSaleRepository = adminSaleRepository;
    }
    public async Task<Result<Unit>> Handle(UpdateAllPaidsCommand request, CancellationToken cancellationToken)
    {
       var sales = await adminSaleRepository.GetAll(cancellationToken);
       foreach(var i in sales)
        {
            i.UpdatePaid();
            await adminSaleRepository.UpdateAsync(i, cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}
