using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Entity.ComboDetails;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Combo.Create;

public class CreateComboEntityCommandHandler : CreateGenericEntityCommandHandler<ComboEntity, CreateComboEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ComboEntity> logger;
    private readonly IComboRepository comboRepository;
    public CreateComboEntityCommandHandler(IComboRepository repository, IMapper mapper , ILogger<ComboEntity> logger , IProductRepository product) : base(repository, mapper)
    {
        comboRepository = repository;
        this.logger = logger;
        productRepository = product;
    }
    public override async Task<Result<Unit>> Handle(CreateComboEntityCommand request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.GetByName(request.Name , cancellationToken);
        if(combo != null)
        {
            logger.LogWarning("Se ha intentado crear un combo con un nombre ya registrado: "+request.Name);
            return Result<Unit>.Failure(new ComboRegisteredError());
        }
        var productsId = request.CreateComboDto.Select(p => p.ProductId).Distinct().ToList();
        var quantityIds = await productRepository.ContainsId(productsId ,cancellationToken);
        if(productsId.Count() != quantityIds)
        {
            logger.LogWarning("Se ha intentado crear un combo ,pero hay algunos productos que no estan registrados");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        var newCombo = ComboEntity.Create(request.Name , request.Price);
        var details = request.CreateComboDto.Select(p => ComboDetailsEntity.Create(newCombo.Id ,p.ProductId ,p.Quantity));
        newCombo.ComboDetails.AddRange(details);
        await comboRepository.AddAsync(newCombo);
        return Result<Unit>.Success(Unit.Value);
    }
}