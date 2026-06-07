using AlmacenEconomia.Application.Command.ProductEnter.CleanOldProductEnter;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using MediatR;
using Microsoft.Extensions.Logging;

public class CleanOldProductEnterCommandHandler : IRequestHandler<CleanOldProductsEnterCommand, Result<int>>
{
    private readonly IProductEnterRepository _repository;
    private readonly ILogger<CleanOldProductEnterCommandHandler> _logger;

    public CleanOldProductEnterCommandHandler(IProductEnterRepository repository, ILogger<CleanOldProductEnterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CleanOldProductsEnterCommand request, CancellationToken cancellationToken)
    {
        int back = 0;
        try
        {
            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
            back = await _repository.DeleteOldEntriesAsync(threeMonthsAgo, cancellationToken);
            
            _logger.LogInformation("{Count} entradas de productos más antiguas que {Date} fueron eliminadas.", back, threeMonthsAgo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al limpiar entradas de productos antiguas.");
        }
        return Result<int>.Success(back);
    }
}