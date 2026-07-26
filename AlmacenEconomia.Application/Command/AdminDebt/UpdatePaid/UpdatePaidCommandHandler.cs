using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminDebt.UpdatePaid;

public class UpdatePaidCommandHandler : UpdateGenericEntityCommandHandler<AdminDebtEntity, UpdatePaidCommand>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly ILogger<AdminDebtEntity> logger;
    public UpdatePaidCommandHandler(IAdminDebtRepository generic, IMapper mapper , ILogger<AdminDebtEntity> logger) : base(generic, mapper)
    {
        adminDebtRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdatePaidCommand request, CancellationToken cancellationToken)
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