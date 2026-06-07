using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Code;
using AlmacenEconomia.Application.Interfaces.Repository.Code;
using AlmacenEconomia.Domain.Entity.Code;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Code.MatchCodeByEmail;

public class MatchCodeByEmailCommandHandler : IRequestHandler<MatchCodeByEmailCommand, Result<Unit>>{
    private readonly ICodeRepository codeRepository;
    private readonly ILogger<CodeEntity> logger;
    private readonly ICodeHash codeHash;
    public MatchCodeByEmailCommandHandler(ICodeRepository code , ILogger<CodeEntity> logger , ICodeHash hash)
    {
        codeRepository = code;
        this.logger = logger;
        codeHash = hash;
    }
    public async Task<Result<Unit>> Handle(MatchCodeByEmailCommand request, CancellationToken cancellationToken)
    {
        var code = await codeRepository.GetCodeByEmail(request.Email , cancellationToken);
        if(code == null)
        {
            logger.LogWarning("El codigo con email: "+request.Email+" no esta registrado");
            return Result<Unit>.Failure(new CodeNotFoundError());
        }
        if(!codeHash.VerifyHash(request.Code , code.Code))
        {
            logger.LogWarning("Codigo incorrecto para el email: "+request.Email);
            return Result<Unit>.Failure(new WrongCodeError());
        }
        if(code.DateTimeExpiration <= DateTime.UtcNow)
        {
            logger.LogWarning("El codigo que el email: "+request.Email+" introdujo ha caducado");
            return Result<Unit>.Failure(new ExpiredCodeError());
        }
        code.ClearCode();
        await codeRepository.UpdateAsync(code , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}