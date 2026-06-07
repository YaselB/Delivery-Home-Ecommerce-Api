namespace AlmacenEconomia.Application.Interfaces.Password;
public interface IPasswordHashed
{
    public string GenerateHash(string password);
    public bool VerifiPassword(string password , string HashPassword);
}