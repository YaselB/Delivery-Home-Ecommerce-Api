using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Common.Behaviour;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerformanceBehaviour<TRequest , TResponse>> logger;
    private readonly Stopwatch stopwatch;
    public PerformanceBehaviour(ILogger<PerformanceBehaviour<TRequest , TResponse>> logger )
    {
        this.logger = logger;
        stopwatch = new Stopwatch();
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        stopwatch.Start();
        var response = await next();
        stopwatch.Stop();
        var elapsed = stopwatch.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;
        if(elapsed > 500)
        {
            logger.LogWarning("⚠️ La petición {RequestName} demoró {ElapsedMs} ms (excede el umbral de 500 ms)", requestName, elapsed);
        }
        else
        {
            logger.LogInformation("✅ La petición {RequestName} demoró {ElapsedMs} ms (tiempo óptimo)", requestName, elapsed);
        }
        return response;
    }
}