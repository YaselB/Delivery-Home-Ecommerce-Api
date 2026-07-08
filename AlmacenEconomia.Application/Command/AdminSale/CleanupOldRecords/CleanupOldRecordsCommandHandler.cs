using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using MediatR;

namespace AlmacenEconomia.Application.Command.AdminSale.CleanupOldRecordsCommand;

public class CleanupOldRecordsCommandHandler : IRequestHandler<CleanupOldRecordsCommand, Result<Unit>>
{
    private readonly IAdminSaleRepository adminSaleRepository;
    public CleanupOldRecordsCommandHandler(IAdminSaleRepository adminSaleRepository)
    {
        this.adminSaleRepository = adminSaleRepository;
    }
    public async Task<Result<Unit>> Handle(CleanupOldRecordsCommand request, CancellationToken cancellationToken)
    {
        var admisSaleOldest = await adminSaleRepository.GetAllEnded(cancellationToken); 
        await adminSaleRepository.RemoveRange(admisSaleOldest , cancellationToken);
        return Result<Unit>.Success(Unit.Value);    
    }
}