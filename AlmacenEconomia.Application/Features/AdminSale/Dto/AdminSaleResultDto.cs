using AlmacenEconomia.Application.Features.AdminSale.AdminSaleDetailsDto;

namespace AlmacenEconomia.Application.Features.AdminSale.Dto;
public class AdminSaleResultDto
{
    public required string Id {get ; set ;}
    public required string Name {get ; set ;}
    public required double Total {get ; set ;}
    public required List<AdminSaleDetailsResultDto> adminSaleDetailsResultDtos;
}