namespace AlmacenEconomia.Domain.ValueObject;
public class PhoneNumber
{
    public string CountryCode {get ; private set;} = string.Empty;
    public string PhoneCode {get ; private set;} = string.Empty;
    private PhoneNumber(){}
    public PhoneNumber(string countryCode , string phonecode)
    {
        if(string.IsNullOrWhiteSpace(countryCode) || !countryCode.StartsWith("+"))
        {
            throw new ArgumentException("El código de país debe comenzar con '+'");
        }
        if(string.IsNullOrWhiteSpace(phonecode) || phonecode.Length < 5 || phonecode.Length > 15)
        {
            throw new ArgumentException("El número de teléfono debe tener entre 5 y 15 dígitos");
        }
        var cleaned = new string(phonecode.Where(char.IsDigit).ToArray());
        CountryCode = countryCode;
        PhoneCode = cleaned;
    }
    public override string ToString()
    {
        return $"{CountryCode} {PhoneCode}";
    }
}