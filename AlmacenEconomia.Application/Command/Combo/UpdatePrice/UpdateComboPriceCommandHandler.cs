using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Combo;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Combo.UpdatePrice;

public class UpdateComboPriceCommandHandler : UpdateGenericEntityCommandHandler<ComboEntity, UpdateComboPriceCommand>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    public UpdateComboPriceCommandHandler(IComboRepository generic, IMapper mapper , ILogger<ComboEntity> logger) : base(generic, mapper)
    {
        comboRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateComboPriceCommand request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.GetById(request.Id , cancellationToken);
        if(combo == null)
        {
            logger.LogWarning("Se ha intentado actualizar el precio de un combo con id: "+request.Id+" inexistente");
            return Result<Unit>.Failure(new ComboNotFoundError());
        }
        combo.UpdatePrice(request.Price);
        await comboRepository.UpdateAsync(combo , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}