using AlmacenEconomia.Application.Features.Combo.ProductItem;

namespace AlmacenEconomia.Application.Features.Combo.ResultDto;
public class ComboResultDto
{
    public required string Id {get ; set ;}
    public required string Name {get ; set ;}
    public required double Price {get ; set ;}
    public required List<ProductItemDto> ProductItems{get ; set ;}
}