using AlmacenEconomia.Application.Interfaces.Password;

namespace AlmacenEconomia.Infrastructure.Security;

public class PasswordHashed : IPasswordHashed
{
    public string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password , workFactor: 10);
    }

    public bool VerifiPassword(string password, string HashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password , HashPassword);
    }
}