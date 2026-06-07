using AlmacenEconomia.Application.Features.Combo.ResultDto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Combo;

namespace AlmacenEconomia.Application.Query.Combo.GetAll;
public class GetAllComboEntityQuery : GetAllGenericEntityQuery<ComboEntity , ComboResultDto>{}