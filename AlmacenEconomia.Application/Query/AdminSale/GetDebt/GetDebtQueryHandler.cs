using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using MediatR;

namespace AlmacenEconomia.Application.Query.AdminSale.GetDebt;

public class GetDebtQueryHandler : IRequestHandler<GetDebtQuery, Result<double>>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    public GetDebtQueryHandler(IAdminSaleRepository adminSaleRepository)
    {
        this.adminSaleRepository = adminSaleRepository;
    }
    public async Task<Result<double>> Handle(GetDebtQuery request, CancellationToken cancellationToken)
    {
        var debt = await adminSaleRepository.GetDebt(cancellationToken);
        return Result<double>.Success(debt);
    }
}