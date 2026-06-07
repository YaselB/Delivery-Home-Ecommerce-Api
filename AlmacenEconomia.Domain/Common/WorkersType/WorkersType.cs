namespace AlmacenEconomia.Domain.Common.WorkersType;
public static class WorkersType
{
    public const string WareHouseworker = "WareHouseworker";
    public const string Seller = "Seller";
    public const string DeliverPerson = "DeliveryPerson";
    public const string Financial = "Financial";
    public static IReadOnlySet<string> AllWorkers => new HashSet<string>
    {
        WareHouseworker,
        Seller,
        DeliverPerson,
        Financial
    };
}