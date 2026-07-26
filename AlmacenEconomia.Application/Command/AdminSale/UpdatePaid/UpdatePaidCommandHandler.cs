using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSale;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminSale.UpdatePaid;

public class UpdatePaidCommandHandler : IRequestHandler<UpdatePaidCommand, Result<Unit>>
{
    private readonly ILogger<AdminSaleEntity> logger;
    private readonly IAdminSaleRepository adminSaleRepository;
    public UpdatePaidCommandHandler( ILogger<AdminSaleEntity> logger , IAdminSaleRepository adminSaleRepository)
    {
        this.logger = logger;
        this.adminSaleRepository = adminSaleRepository;
    }
    public async Task<Result<Unit>> Handle(UpdatePaidCommand request, CancellationToken cancellationToken)
    {
        var sales = await adminSaleRepository.GetListEntities(request.SalesId , cancellationToken);
        if(sales.Count != request.SalesId.Count)
        {
            logger.LogWarning("Algunas salidas no han sido encontradas");
            return Result<Unit>.Failure(new AdmisSalesNotFoundError());
        }
        if(sales.Any(a => a.AdminId != request.AdminId))
        {
            logger.LogWarning("Algunas salidas no pertenecen al administrador asignado");
            return Result<Unit>.Failure(new AdminSaleNotMatchWithAdminIDError());
        }
        foreach(var sale in sales)
        {
            sale.UpdatePaid();
            await adminSaleRepository.UpdateAsync(sale , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}