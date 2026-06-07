namespace AlmacenEconomia.Application.Features.Admin.Dto;
public class AdminResultDto
{
    public required string Id {get ; set ;}
    public required string Email {get ; set ;}
    public required List<string> Permission {get ; set ;}
}