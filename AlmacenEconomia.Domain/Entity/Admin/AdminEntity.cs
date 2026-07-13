using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Events.Admin.AddPermissions;
using AlmacenEconomia.Domain.Events.Admin.Create;
using AlmacenEconomia.Domain.Events.Admin.Update;

namespace AlmacenEconomia.Domain.Entity.Admin;
public class AdminEntity : GenericEntity<AdminEntity>
{
    public string Email {get ; set ;} = string.Empty;
    public string Password {get ; set ;} = string.Empty;
    public ICollection<AdminSaleEntity>? AdminSaleEntity{get ; set ;}
    public string PermissionJson {get ; set ;} = "[]";
    [NotMapped]
    public IReadOnlyList<string> Permission => JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
    public void AddPermission(List<string> permissions)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
        foreach(var i in permissions)
        {
            if(!list.Contains(i)){
                list.Add(i);
            }
        }
        PermissionJson = JsonSerializer.Serialize(list);
        this.UpdatedAt = DateTime.UtcNow;
        var AddPermissionAdminDomainEvent = new AddPermissionsAdminEntityEvent(this.Id , this.Email);
        this.AddDomainEvent(AddPermissionAdminDomainEvent);
    }
    public static AdminEntity Create(string Email , string password)
    {
        var admin = new AdminEntity
        {
            Password = password,
            Email = Email
        };
        var permissions = new List<string>
        {
            Permissions.CreateAdminPermission,
            Permissions.AddPermissionToAdmin,
            Permissions.DeleteAdminPermission,
            Permissions.GetOnlyAdminPermission,
            Permissions.GetAllAdminPermission,
            Permissions.RevokePermissionToAdmin,
            Permissions.AddPermissionToCustomer,
            Permissions.AddPermissionToCustomer,
            Permissions.RevokePermissionToCustomer,
            Permissions.GetAllCustomersPermission,
            Permissions.GetOnlyCustomerPermission,
            Permissions.CreateWorkerPermission,
            Permissions.AddWorkerPermission,
            Permissions.RevokeWorkerPermission,
            Permissions.GetAllWorkersPermission,
            Permissions.GetOnlyWorkerPermission,
            Permissions.UpdateWorkerJobPermission,
            Permissions.GetAllPermissions,
            Permissions.GetAllJobsPermission,
            Permissions.Auth,
            Permissions.CreateProductPermission,
            Permissions.UpdateProductPermission,
            Permissions.DeleteProductPermission,
            Permissions.GetAllProductsPermission,
            Permissions.GetOnlyProductPermission,
            Permissions.CreateComboPermission,
            Permissions.UpdateComboPermission,
            Permissions.DeleteComboPermission,
            Permissions.GetAllComboPermission,
            Permissions.GetOnlyComboPermission,
            Permissions.CreateOfferPermission,
            Permissions.UpdateOfferPermission,
            Permissions.DeleteOfferPermission,
            Permissions.GetOnlyOfferPermission,
            Permissions.GetAllOfferPermission
        };
        admin.PermissionJson = JsonSerializer.Serialize(permissions);
        var createAdminDomainEvent = new CreateAdminEntityEvent(admin.Id , admin.Email);
        admin.AddDomainEvent(createAdminDomainEvent);
        return admin;
    }
    public void Update(string password)
    {
        this.Password = password;
        var updateAdminDomainEvent = new UpdateAdminEntityEvent(this.Id ,this.Email);
        this.AddDomainEvent(updateAdminDomainEvent);
    }
    public void RevokePermission(List<string> permissions)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
        foreach( var i in permissions)
        {
            if (list.Contains(i))
            {
                list.Remove(i);
            }
        }
        PermissionJson = JsonSerializer.Serialize(list);

    }
}