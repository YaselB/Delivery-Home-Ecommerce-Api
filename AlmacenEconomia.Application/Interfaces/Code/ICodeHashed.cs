namespace AlmacenEconomia.Application.Interfaces.Code;
public interface ICodeHash
{
    public string GenerateHash(string code);
    public bool VerifyHash(string code , string hash);
}