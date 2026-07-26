using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSale;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminSale.UpdateAllPaids;

public class UpdateAllPaidsCommandHandler : IRequestHandler<UpdateAllPaidsCommand, Result<Unit>>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    private readonly ILogger<AdminSaleEntity> logger;
    public UpdateAllPaidsCommandHandler(IAdminSaleRepository adminSaleRepository , ILogger<AdminSaleEntity> logger)
    {
        this.adminSaleRepository = adminSaleRepository;
        this.logger = logger;
    }
    public async Task<Result<Unit>> Handle(UpdateAllPaidsCommand request, CancellationToken cancellationToken)
    {
       var sales = await adminSaleRepository.GetAll(cancellationToken);
       foreach(var i in sales)
        {
            if(i.AdminId != request.AdminId)
            {
                logger.LogWarning("La salida con id: "+i.Id+" no pertenece al administrador con id: "+request.AdminId);
                return Result<Unit>.Failure(new AdminSaleNotMatchWithAdminIDError());
            }
        }
       foreach(var i in sales)
        {
            i.UpdatePaid();
            await adminSaleRepository.UpdateAsync(i, cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}
