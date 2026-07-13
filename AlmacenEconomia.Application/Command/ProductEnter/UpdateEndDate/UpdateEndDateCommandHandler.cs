using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateEndDate;

public class UpdateEndDateCommandHandler : UpdateGenericEntityCommandHandler<ProductEnterEntity, UpdateEndDateCommand>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly ILogger<ProductEnterEntity> logger;
    public UpdateEndDateCommandHandler(IProductEnterRepository generic, IMapper mapper , ILogger<ProductEnterEntity> logger) : base(generic, mapper)
    {
        productEnterRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateEndDateCommand request, CancellationToken cancellationToken)
    {
        var enter = await productEnterRepository.GetById(request.Id , cancellationToken);
        if(enter == null)
        {
            logger.LogWarning("Se ha intentado actualizar la fecha de vencimiento de la entrada :"+request.Id+" pero esa entrada no esta registrada");
            return Result<Unit>.Failure(new ProductEnterNotFoundError());
        }
        enter.UpdateEndDate(request.EndDate);
        await productEnterRepository.UpdateAsync(enter , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}