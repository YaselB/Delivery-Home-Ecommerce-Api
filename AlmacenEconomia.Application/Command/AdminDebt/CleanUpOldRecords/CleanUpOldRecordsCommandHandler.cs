using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using MediatR;

namespace AlmacenEconomia.Application.Command.AdminDebt.CleanUpOldRecords;

public class CleanupOldRecordsCommandHandler : IRequestHandler<CleanupOldRecordsCommand, Result<Unit>>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    public CleanupOldRecordsCommandHandler(IAdminDebtRepository repository)
    {
        adminDebtRepository = repository;
    }
    public async Task<Result<Unit>> Handle(CleanupOldRecordsCommand request, CancellationToken cancellationToken)
    {
        var oldRecords = await adminDebtRepository.GetAllEnded(cancellationToken);
        await adminDebtRepository.RemoveRange(oldRecords ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}