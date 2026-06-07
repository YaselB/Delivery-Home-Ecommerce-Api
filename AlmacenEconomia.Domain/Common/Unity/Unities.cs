namespace AlmacenEconomia.Domain.Common.Unity;
public static class Unities
{
    public const string Unity = "u";
    public const string Kilograms = "kg";
    public const string Pounds = "lb";
    public static IReadOnlySet<string> AllUnities => new  HashSet<string>
    {
        Unity,
        Kilograms,
        Pounds
    };
    public static double Convert(string unit , double value)
    {
        if(unit == "lb")
        {
            value = Math.Round(value * 2.17 ,2);
            return value;
        }
        value = Math.Round(value / 2.17 ,2);
        return value;
    }
}