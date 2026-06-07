namespace AlmacenEconomia.Domain.Common.ProductSections;

public static class ProductSections
{
    public const string PersonalCare = "PersonalCare";
    public const string HouseHoldCleaning = "HouseHoldCleaning";
    public const string Meat = "Meat";
    public const string Beverages = "Beverages";
    public const string Dairy = "Dairy";
    public const string Pantry = "Pantry";
    public static IReadOnlySet<string> AllSections => new HashSet<string>
    {
        PersonalCare,
        HouseHoldCleaning,
        Meat,
        Beverages,
        Dairy,
        Pantry
    };
}