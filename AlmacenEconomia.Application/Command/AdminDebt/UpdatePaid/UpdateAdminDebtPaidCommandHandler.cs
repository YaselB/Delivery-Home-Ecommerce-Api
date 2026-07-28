using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdatePaid;

public class UpdatePaidCommandHandler : IRequestHandler<UpdateAdminDebtPaidCommand , Result<Unit>>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly ILogger<AdminDebtEntity> logger;
    public UpdatePaidCommandHandler(IAdminDebtRepository generic , ILogger<AdminDebtEntity> logger)
    {
        adminDebtRepository = generic;
        this.logger = logger;
    }
    public async Task<Result<Unit>> Handle(UpdateAdminDebtPaidCommand request, CancellationToken cancellationToken)
    {
        var adminDebt = await adminDebtRepository.GetDebtByIds(request.DebtIds , cancellationToken);
        if(adminDebt.Count != request.DebtIds.Count)
        {
            logger.LogWarning("Algunos prestamos no estan registrados");
            return Result<Unit>.Failure(new AdminsDebtNotFoundError());
        }
        if(adminDebt.Any(a => a.AdminId != request.AdminId))
        {
            logger.LogWarning("Algunos prestamos no estan asignados a este admin : "+request.AdminId);
            return Result<Unit>.Failure(new AdminDebtNotRegisteredAdminIdError());
        }
        foreach(var i in adminDebt)
        {
            i.UpdatePaid();
            await adminDebtRepository.UpdateAsync(i , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}