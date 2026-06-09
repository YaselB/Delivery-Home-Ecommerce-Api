using AlmacenEconomia.Application.Features.HomeSale.HomeSaleDetails;

namespace AlmacenEconomia.Application.Features.HomeSale.ResultDto;
public class HomeSaleResultDto
{
    public required string Id {get ; set ;}
    public required double Total {get ; set ;}
    public required List<HomeSaleDetailsResultDto> HomeSaleDetails {get ; set ;}
}