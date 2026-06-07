using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.ProductEnter.UpdateCode;

public class UpdateCodeCommandHandler : UpdateGenericEntityCommandHandler<ProductEnterEntity, UpdateCodeCommand>
{
    private readonly IProductEnterRepository productEnterRepository;
    private readonly ILogger<ProductEnterEntity> logger;
    public UpdateCodeCommandHandler(IProductEnterRepository generic, IMapper mapper , ILogger<ProductEnterEntity> logger) : base(generic, mapper)
    {
        productEnterRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateCodeCommand request, CancellationToken cancellationToken)
    {
        var enter = await productEnterRepository.GetById(request.Id , cancellationToken);
        if(enter == null)
        {
            logger.LogWarning("La entrada con id: "+request.Id+" no esta registrada");
            return Result<Unit>.Failure(new ProductEnterNotFoundError());
        }
        var code = await productEnterRepository.GetByCode(request.Code ,enter.ProductId , cancellationToken);
        if(code != null)
        {
            logger.LogWarning("el codigo de entrada: "+request.Code+" ya esta registrado");
            return Result<Unit>.Failure(new CodeEnterRegisteredError());
        }
        enter.UpdateCode(request.Code);
        await productEnterRepository.UpdateAsync(enter , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}