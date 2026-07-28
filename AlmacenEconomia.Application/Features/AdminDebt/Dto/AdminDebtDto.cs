namespace AlmacenEconomia.Application.Features.AdminDebt.Dto;
public class AdminDebtDto
{
    public required string Id { get ; set ;}
    public required double Debt {get ; set ;}
    public required bool Paid {get ; set ;}
    public required string Email {get ; set ;}
}