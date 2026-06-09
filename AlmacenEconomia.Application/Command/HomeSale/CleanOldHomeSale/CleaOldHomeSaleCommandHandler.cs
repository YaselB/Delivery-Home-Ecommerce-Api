using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AlmacenEconomia.Domain.Entity.HomeSale;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.HomeSale.CleanOldHomeSale;

public class CleanOldHomeSaleCommandHandler : IRequestHandler<CleanOldHomeSaleCommand, Result<int>>
{
    private readonly IHomeSaleRepository homeSaleRepository;
    private readonly ILogger<HomeSaleEntity> logger;
    public CleanOldHomeSaleCommandHandler(IHomeSaleRepository homeSale , ILogger<HomeSaleEntity> logger)
    {
        homeSaleRepository = homeSale;
        this.logger = logger;
    }
    public async Task<Result<int>> Handle(CleanOldHomeSaleCommand request, CancellationToken cancellationToken)
    {
        int back = 0;
        try
        {
            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
            back = await homeSaleRepository.DeleteOldestEntities(threeMonthsAgo , cancellationToken);
            logger.LogInformation("Se han eliminado "+back+" salidas de producto hacia la casa");
        }
        catch(Exception ex)
        {
            logger.LogError(ex , "Error al limpiar las salidas de productos de la casa");
        }
        return Result<int>.Success(back);
    }
}