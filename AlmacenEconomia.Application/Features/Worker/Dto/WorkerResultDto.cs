namespace AlmacenEconomia.Application.Features.Worker.Dto;
public class WorkerResultDto
{
    public required string Email {get ; set ;}
    public required string Id {get ; set ; }
    public required List<string> Permission {get ; set ;}
    public required string Job {get ; set ;}
}