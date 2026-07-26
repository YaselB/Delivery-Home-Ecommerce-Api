using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdateAllPaids;

public class UpdateAllPaidsCommandHandler : UpdateGenericEntityCommandHandler<AdminDebtEntity, UpdateAllPaidsCommand>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly ILogger<AdminDebtEntity> logger;
    public UpdateAllPaidsCommandHandler(IAdminDebtRepository generic, IMapper mapper , ILogger<AdminDebtEntity> logger) : base(generic, mapper)
    {
        adminDebtRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateAllPaidsCommand request, CancellationToken cancellationToken)
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