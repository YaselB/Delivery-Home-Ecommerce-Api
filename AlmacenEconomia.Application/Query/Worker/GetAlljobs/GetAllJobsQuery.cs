using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Query.Worker.GetAllJobs;
public class GetAllJobsQuery : IRequest<Result<List<string>>>
{
    
}