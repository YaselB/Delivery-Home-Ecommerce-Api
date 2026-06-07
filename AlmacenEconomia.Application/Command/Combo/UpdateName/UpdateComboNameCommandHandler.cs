using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Combo;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Combo.UpdateName;

public class UpdateComboNameCommandHandler : UpdateGenericEntityCommandHandler<ComboEntity, UpdateComboNameCommand>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    public UpdateComboNameCommandHandler(IComboRepository generic, IMapper mapper , ILogger<ComboEntity> logger) : base(generic, mapper)
    {
        comboRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateComboNameCommand request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.GetById(request.Id ,cancellationToken);
        if(combo == null)
        {
            logger.LogWarning("Se ha intentado actualizar el nombre de un combo inexistente con id: "+request.Id);
            return Result<Unit>.Failure(new ComboNotFoundError());
        }
        var name = await comboRepository.GetByName(request.Name ,cancellationToken);
        if(name != null)
        {
            logger.LogWarning("Se ha intentado actualizar el nombre de un combo con nombre: "+request.Name+" pero ese nombre ya ha sido usado");
            return Result<Unit>.Failure(new ComboNameRegisteredError());
        }
        combo.UpdateName(request.Name);
        await comboRepository.UpdateAsync(combo , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}