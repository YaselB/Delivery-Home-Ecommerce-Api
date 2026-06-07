using System.Diagnostics;
using MediatR;
using AlmacenEconomia.Application.Command.ProductEnter.CleanOldProductEnter;

namespace AlmacenEconomia.Presentation.Services.Background;
public class ProductEnterCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProductEnterCleanupService> _logger;

    public ProductEnterCleanupService(IServiceScopeFactory serviceScopeFactory, ILogger<ProductEnterCleanupService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Product Enter Cleanup Service is starting.");
        var now = DateTime.UtcNow;
        var nextRun = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(1);
        var initialDelay = nextRun - now;

        _logger.LogInformation("Initial delay set to {Delay}. First run scheduled at {NextRun}", initialDelay, nextRun);

        if (initialDelay.TotalMilliseconds > 0)
        {
            await Task.Delay(initialDelay, stoppingToken);
        }
        using var timer = new PeriodicTimer(TimeSpan.FromDays(30));
        try
        {
            do
            {
                await EjecutarLimpiezaAsync(stoppingToken);
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Product Enter Cleanup Service is stopping.");
        }
    }

    private async Task EjecutarLimpiezaAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ProductEnterCleanupService>>();

            logger.LogInformation("Executing scheduled cleanup for old product enters.");
            var stopwatch = Stopwatch.StartNew();
            var result = await mediator.Send(new CleanOldProductsEnterCommand(), stoppingToken);
            stopwatch.Stop();

            if (result.IsFailure && result.error != null)
                logger.LogError("Cleanup failed: {Error}. Execution time: {Elapsed}ms", result.error.Message, stopwatch.ElapsedMilliseconds);
            else
                logger.LogInformation("Cleanup completed successfully. {Count} records deleted in {Elapsed}ms.", result.Value, stopwatch.ElapsedMilliseconds);
        }
    }
}