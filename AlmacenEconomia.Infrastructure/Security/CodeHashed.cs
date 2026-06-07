using AlmacenEconomia.Application.Interfaces.Code;

namespace AlmacenEconomia.Infrastructure.Security;

public class CodeHash : ICodeHash
{
    public string GenerateHash(string code)
    {
        return BCrypt.Net.BCrypt.HashPassword(code ,workFactor: 10);
    }

    public bool VerifyHash(string code, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(code ,hash);
    }
}