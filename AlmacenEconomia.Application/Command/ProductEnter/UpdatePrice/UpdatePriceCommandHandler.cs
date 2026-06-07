using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdatePriceCup;

public class UpdatePriceCommandHandler : UpdateGenericEntityCommandHandler<ProductEnterEntity, UpdatePriceCommand>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly ILogger<ProductEnterEntity> logger;
    public UpdatePriceCommandHandler(IProductEnterRepository generic, IMapper mapper , ILogger<ProductEnterEntity> logger) : base(generic, mapper)
    {
        productEnterRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
    {
        var enter = await productEnterRepository.GetById(request.Id , cancellationToken);
        if(enter == null)
        {
            logger.LogWarning("La entrada con id: "+request.Id+" no esta registrada");
            return Result<Unit>.Failure(new ProductEnterNotFoundError());
        }
        var newPriceUsd = Math.Round(request.PriceCup / request.PriceUsd , 2);
        enter.UpdatePriceCup(request.PriceCup , newPriceUsd);
        await productEnterRepository.UpdateAsync(enter , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}