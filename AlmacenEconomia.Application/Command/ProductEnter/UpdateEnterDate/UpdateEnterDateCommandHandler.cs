using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateEnterDate;

public class UpdateEnterDateCommandHandler : UpdateGenericEntityCommandHandler<ProductEnterEntity, UpdateEnterDateCommand>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly ILogger<ProductEnterEntity> logger;
    public UpdateEnterDateCommandHandler(IProductEnterRepository generic, IMapper mapper , ILogger<ProductEnterEntity> logger) : base(generic, mapper)
    {
        productEnterRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateEnterDateCommand request, CancellationToken cancellationToken)
    {
        var enter = await productEnterRepository.GetById(request.Id , cancellationToken);
        if(enter == null)
        {
            logger.LogWarning("La entrada con id: "+request.Id+" no esta registrada para actualizar su fecha de entrada");
            return Result<Unit>.Failure(new ProductEnterNotFoundError());
        }
        enter.UpdateEnterDate(request.EnterDate);
        await productEnterRepository.UpdateAsync(enter , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}