using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdateAllPaids;

public class UpdateAllAdminDebtPaidsCommandHandler : IRequestHandler<UpdateAllAdminDebtPaidsCommand , Result<Unit>>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly ILogger<AdminDebtEntity> logger;
    public UpdateAllAdminDebtPaidsCommandHandler(IAdminDebtRepository generic , ILogger<AdminDebtEntity> logger)
    {
        adminDebtRepository = generic;
        this.logger = logger;
    }
    public async Task<Result<Unit>> Handle(UpdateAllAdminDebtPaidsCommand request, CancellationToken cancellationToken)
    {
        var debts = await adminDebtRepository.GetAllPendigs(cancellationToken);
        if(debts.Any(a => a.AdminId != request.AdminId))
        {
            logger.LogWarning("Algunos prestamos no estan asignados a ese admin");
            return Result<Unit>.Failure(new AdminDebtNotRegisteredAdminIdError());
        }
        foreach(var i in debts)
        {
            i.UpdatePaid();
            await adminDebtRepository.UpdateAsync(i , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}