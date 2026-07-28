using System.Reflection.Metadata;

namespace AlmacenEconomia.Domain.Common.Permission;
public static class Permissions
{
    public const string CreateAdminPermission = "CreateAdminPermission";
    public const string AddPermissionToAdmin = "AddPermissionToAdmin";
    public const string RevokePermissionToAdmin = "RevokePermissionToAdmin";
    public const string DeleteAdminPermission = "DeleteAdminPermission";
    public const string GetOnlyAdminPermission = "GetOnlyAdminPermission";
    public const string GetAllAdminPermission = "GetAllAdminPermission";
    public const string AddPermissionToCustomer = "AddPermissionToCustomer";
    public const string RevokePermissionToCustomer = "RevokePermissionToCustomer";
    public const string GetAllCustomersPermission = "GetAllCustomerPermission";
    public const string GetOnlyCustomerPermission = "GetOnlyCustomerPermission";
    public const string DeleteCustomerPermission = "DeleteCustomerPermission";
    public const string CreateWorkerPermission = "CreateWorkerPermission";
    public const string AddWorkerPermission = "AddWorkerPermission";
    public const string RevokeWorkerPermission = "RevokeWorkerPermission";
    public const string DeleteWorkerPermission = "DeleteWorkerPermission";
    public const string GetAllWorkersPermission = "GetAllWorkersPermission";
    public const string GetOnlyWorkerPermission = "GetOnlyWorkerPermission";
    public const string UpdateWorkerJobPermission = "UpdateWorkerJobPermission";
    public const string GetAllPermissions = "GetAllPermissions";
    public const string GetAllJobsPermission = "GetAllJobsPermission";
    public const string Auth = "Auth";
    public const string CreateProductPermission = "CreateProductPermission";
    public const string UpdateProductPermission = "UpdateProductPermission";
    public const string DeleteProductPermission = "DeleteProductPermission";
    public const string GetAllProductsPermission = "GetAllProductsPermission";
    public const string GetOnlyProductPermission = "GetOnlyProductPermission";
    public const string GetAllSections = "GetAllSections";
    public const string CreateComboPermission = "CreateComboPermission";
    public const string UpdateComboPermission = "UpdateComboPermission";
    public const string DeleteComboPermission = "DeleteComboPermission";
    public const string GetAllComboPermission = "GetAllComboPermission";
    public const string GetOnlyComboPermission = "GetOnlyComboPermission";
    public const string CreateOfferPermission = "CreateOfferPermission";
    public const string UpdateOfferPermission = "UpdateOfferPermission";
    public const string DeleteOfferPermission = "DeleteOfferPermission";
    public const string GetOnlyOfferPermission = "GetOnlyOfferPermission";
    public const string GetAllOfferPermission = "GetAllOfferPermission";
    public const string CreateProductEnterPermission = "CreateProductEnterPermission";
    public const string UpdateProductEnterPermission = "UpdateProductEnterPermission";
    public const string GetOnlyProductEnterPermission = "GetOnlyProductEnterPermission";
    public const string GetAllProductEnterPermission = "GetAllProductEnterPermission";
    public const string GetEnterByProductIdPermission = "GetEnterByIdProductIdPermission";
    public const string CreateHomeSalePermission = "CreateHomeSalePermission";
    public const string UpdateHomeSalePermission = "UpdateHomeSalePermission";
    public const string GetOnlyHomeSalePermission = "GetOnlyHomeSalePermission";
    public const string GetAllHomeSalePermission = "GetAllHomeSalePermission";
    public const string GetHomeSaleByProductId = "GetHomeSaleByProductId";
    public const string CreateAdminSalePermission = "CreateAdminSalePermission";
    public const string UpdateAdminSalePermission = "UpdateAdminSalePermission";
    public const string DeleteAdminSalePermission = "DeleteAdminSalePermission";
    public const string GetOnlyAdminSalePermission = "GetOnlyAdminSalePermission";
    public const string GetAllAdminSalePermission = "GetAllAdminSalePermission";
    public const string GetAdminSaleByProductIdPermission = "GetAdminSaleByProductIdPermission";
    public const string CreateAdminDebtPermission = "CreateAdminDebtPermission";
    public const string UpdateAdminDebtPermission = "UpdateAdminDebtPermission";
    public const string DeleteAdminDebtPermission = "DeleteAdminSalePermission";
    public const string GetOnlyAdminDebtPermission = "GetonlyAdminDebtPermission";
    public const string GetAllAdminDebtPermission = "GetAllAdminDebtPermission";
    public const string GetAdminDebtByAdminIdPermission = "GetAdminDebtByAdminIdPermission";
    public const string GetAdminSaleDebtPermission = "GetAdminSaleDebtPermission";
    public static IReadOnlySet<string> AllAdminPermissions => new HashSet<string>
    {
        CreateAdminPermission,
        AddPermissionToAdmin,
        DeleteAdminPermission,
        GetOnlyAdminPermission,
        GetAllAdminPermission,
        RevokePermissionToAdmin,
        RevokePermissionToCustomer,
        AddPermissionToCustomer,
        GetAllCustomersPermission,
        GetOnlyCustomerPermission,
        DeleteCustomerPermission,
        CreateWorkerPermission,
        AddWorkerPermission,
        RevokeWorkerPermission,
        DeleteWorkerPermission,
        GetAllWorkersPermission,
        GetOnlyWorkerPermission,
        UpdateWorkerJobPermission,
        GetAllPermissions,
        GetAllJobsPermission,
        CreateProductPermission,
        UpdateProductPermission,
        DeleteProductPermission,
        GetAllProductsPermission,
        GetOnlyProductPermission,
        GetAllSections,
        CreateComboPermission,
        UpdateComboPermission,
        DeleteComboPermission,
        GetAllComboPermission,
        GetOnlyComboPermission,
        CreateOfferPermission,
        UpdateOfferPermission,
        DeleteOfferPermission,
        GetOnlyOfferPermission,
        GetAllOfferPermission,
        CreateProductEnterPermission,
        UpdateProductEnterPermission,
        GetOnlyProductEnterPermission,
        GetAllProductEnterPermission,
        GetEnterByProductIdPermission,
        CreateHomeSalePermission,
        UpdateHomeSalePermission,
        GetOnlyHomeSalePermission,
        GetAllHomeSalePermission,
        GetHomeSaleByProductId,
        CreateAdminSalePermission,
        UpdateAdminSalePermission,
        DeleteAdminSalePermission,
        GetOnlyAdminSalePermission,
        GetAllAdminSalePermission,
        GetAdminSaleByProductIdPermission,
        CreateAdminDebtPermission,
        UpdateAdminDebtPermission,
        DeleteAdminDebtPermission,
        GetOnlyAdminDebtPermission,
        GetAllAdminDebtPermission,
        GetAdminDebtByAdminIdPermission,
        GetAdminSaleDebtPermission,
    };
    public static IReadOnlySet<string> AllCustomerPermissions => new HashSet<string>
    {
        DeleteCustomerPermission,
        GetOnlyCustomerPermission,
        GetAllProductsPermission,
        GetOnlyProductPermission,
        GetOnlyComboPermission,
        GetAllComboPermission,
        GetOnlyOfferPermission,
        GetAllOfferPermission,
    };
    public static IReadOnlySet<string> AllWorkerPermissions => new HashSet<string>
    {
        DeleteWorkerPermission,
        GetOnlyWorkerPermission,
        CreateProductPermission,
        UpdateProductPermission,
        DeleteProductPermission,
        GetAllProductsPermission,
        GetOnlyProductPermission,
        GetOnlyComboPermission,
        GetAllComboPermission,
        GetOnlyOfferPermission,
        GetAllOfferPermission,
        CreateProductEnterPermission,
        UpdateProductEnterPermission,
        GetOnlyProductEnterPermission,
        GetAllProductEnterPermission,
        GetEnterByProductIdPermission,
        CreateHomeSalePermission,
        UpdateHomeSalePermission,
        GetOnlyHomeSalePermission,
        GetAllHomeSalePermission,
        GetHomeSaleByProductId,
        CreateAdminSalePermission,
        UpdateAdminSalePermission,
        DeleteAdminSalePermission,
        GetOnlyAdminSalePermission,
        GetAllAdminSalePermission,
        GetAdminSaleByProductIdPermission,
        CreateAdminDebtPermission,
        UpdateAdminDebtPermission,
        DeleteAdminDebtPermission,
        GetOnlyAdminDebtPermission,
        GetAllAdminDebtPermission,
        GetAdminDebtByAdminIdPermission,
        GetAdminSaleDebtPermission,
    };
}