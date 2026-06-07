using AlmacenEconomia.Application.Interfaces.Services.CountryValidator;
using CountryData.Globalization.Services;

namespace AlmacenEconomia.Infrastructure.Services.CountryCode;

public class CountryCodeValidator : ICountryValidator
{
    private readonly ICountryDataProvider countryDataProvider;
    public CountryCodeValidator(ICountryDataProvider countryDataProvider)
    {
        this.countryDataProvider = countryDataProvider;
    }
    public bool IsValidCountryCode(string code)
    {
        return countryDataProvider.GetAllCountries().Any( c=> c.PhoneCode == code);
    }
}