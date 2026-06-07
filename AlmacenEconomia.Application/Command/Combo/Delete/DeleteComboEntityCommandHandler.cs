using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Events.Combo.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Combo.Delete;

public class DeleteComboEntityCommandHandler : DeleteGenericEntityCommandHandler<ComboEntity, DeleteComboEntityCommand>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    public DeleteComboEntityCommandHandler(IComboRepository genericRepository ,ILogger<ComboEntity> logger) : base(genericRepository)
    {
        comboRepository = genericRepository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(DeleteComboEntityCommand request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.GetById(request.Id , cancellationToken);
        if(combo == null)
        {
            logger.LogWarning("El combo con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new ComboNotFoundError());
        }
        var DeleteComboDomainEvent = new DeleteComboEntityEvent(combo.Id ,combo.Name);
        combo.AddDomainEvent(DeleteComboDomainEvent);
        await comboRepository.DeleteAsync(combo , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}