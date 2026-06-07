using AlmacenEconomia.Application.Features.Worker.Dto;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.Worker.WorkerProfile;
public class WorkerProfile : Profile
{
    public WorkerProfile()
    {
        CreateMap<WorkerEntity ,WorkerResultDto>()
        .ReverseMap();
    }
}