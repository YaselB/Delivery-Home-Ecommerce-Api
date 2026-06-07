using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateCode;
public class UpdateCodeCommand : UpdateGenericEntityCommand<ProductEnterEntity>
{
    public required string Code {get ; set ;}
}