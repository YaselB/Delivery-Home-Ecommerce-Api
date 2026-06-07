using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Common.WorkersType;
using MediatR;

namespace AlmacenEconomia.Application.Query.Worker.GetAllJobs;

public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, Result<List<string>>>
{
    public Task<Result<List<string>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = WorkersType.AllWorkers.ToList();
        return Task.FromResult(Result<List<string>>.Success(jobs));
    }
}