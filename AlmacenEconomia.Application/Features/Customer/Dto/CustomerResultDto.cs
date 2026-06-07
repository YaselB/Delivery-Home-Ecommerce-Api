namespace AlmacenEconomia.Application.Features.Customer.Dto;
public class CustomerResultDto
{
    public required string Id {get ; set ;}
    public required string Email {get ; set ; }
    public required List<string> Permission {get ; set ;}
}