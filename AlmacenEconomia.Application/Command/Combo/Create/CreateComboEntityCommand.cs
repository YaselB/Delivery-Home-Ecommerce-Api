using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Features.Combo.CreateDto;
using AlmacenEconomia.Domain.Entity.Combo;

namespace AlmacenEconomia.Application.Command.Combo.Create;
public class CreateComboEntityCommand : CreateGenericEntityCommand<ComboEntity>
{
    public string Name { get ; set ;} = string.Empty;
    public double Price {get ; set ;}
    public List<CreateComboDto> CreateComboDto {get ; set ;} = new List<CreateComboDto>();
}