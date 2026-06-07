using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Code;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.Code;
using AlmacenEconomia.Domain.Entity.Code;
using AlmacenEconomia.Domain.Events.Code.Create;
using AlmacenEconomia.Domain.Events.Code.Update;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Code.CreateOrUpdateCommand;

public class CreateOrUpdateCodeCommandHandler : IRequestHandler<CreateOrUpdateCommand, Result<Unit>>
{
    private readonly ICodeRepository codeRepository;
    private readonly ILogger<CodeEntity> logger;
    private readonly ICodeHash codeHash;
    public CreateOrUpdateCodeCommandHandler(ICodeRepository repository , ILogger<CodeEntity> logger , ICodeHash code)
    {
        codeRepository = repository;
        this.logger = logger;
        codeHash = code;
    }
    public async Task<Result<Unit>> Handle(CreateOrUpdateCommand request, CancellationToken cancellationToken)
    {
        var code = await codeRepository.GetCodeByEmail(request.Email , cancellationToken);
        var random = new Random();
        var newCode = random.Next(100000 , 999999).ToString("D6");
        var codeHashed = codeHash.GenerateHash(newCode);
        if(code == null)
        {
           var codeEntity = CodeEntity.Create(codeHashed ,request.Email);
           var CreateCodeDomainEvent = new CreateCodeEntityEvent(request.Email ,newCode ,codeEntity.DateTimeExpiration);
           codeEntity.AddDomainEvent(CreateCodeDomainEvent);
           await codeRepository.AddAsync(codeEntity , cancellationToken);
           return Result<Unit>.Success(Unit.Value);
        }
        code.Update(codeHashed);
        var UpdateCodeDomainEvent = new UpdateCodeEntityEvent(newCode ,request.Email ,code.DateTimeExpiration);
        code.AddDomainEvent(UpdateCodeDomainEvent);
        await codeRepository.UpdateAsync(code , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}