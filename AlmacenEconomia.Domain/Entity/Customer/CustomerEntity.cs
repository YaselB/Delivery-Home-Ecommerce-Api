using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Events.Customer.AddPermissions;
using AlmacenEconomia.Domain.Events.Customer.Create;
using AlmacenEconomia.Domain.Events.Customer.RevokePermissions;
using AlmacenEconomia.Domain.Events.Customer.UpdatePassword;

namespace AlmacenEconomia.Domain.Entity.Customer;
public class CustomerEntity : GenericEntity<CustomerEntity>
{
    public string Email {get ; set ;} = string.Empty;
    public string Password {get ; set ;} = string.Empty;
    public string PermissionJson {get ; set ;} = "[]";
    [NotMapped]
    public IReadOnlyList<string> Permission => JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>(); 
    public static CustomerEntity Create(string email, string password)
    {
        var customer = new CustomerEntity
        {
            Email = email,
            Password = password
        };
        var permissions = new List<string>
        {
            Permissions.DeleteCustomerPermission,
            Permissions.GetOnlyCustomerPermission,
            Permissions.Auth,
            Permissions.GetAllProductsPermission,
            Permissions.GetOnlyProductPermission,
            Permissions.GetOnlyComboPermission,
            Permissions.GetAllComboPermission,
            Permissions.GetAllOfferPermission,
            Permissions.GetOnlyOfferPermission
        };
        customer.PermissionJson = JsonSerializer.Serialize(permissions);
        var CreateCustomerDomainEvent = new CreateCustomerEntityEvent(customer.Email ,customer.Id);
        customer.AddDomainEvent(CreateCustomerDomainEvent);
        return customer;
    }
    public void UpdatePassword(string Password)
    {
        this.Password = Password;
        this.UpdatedAt = DateTime.UtcNow;
        var UpdatePasswordDomainEvent = new UpdateCustomerPasswordEvent(this.Id , this.Email);
        this.AddDomainEvent(UpdatePasswordDomainEvent);
    }
    public void AddPermission(List<string> permissions)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
        foreach(var i in permissions)
        {
            if (!list.Contains(i))
            {
                list.Add(i);
            }
        }
        PermissionJson = JsonSerializer.Serialize(list);
        this.UpdatedAt = DateTime.UtcNow;
        var AddPermissionDomainEvent = new AddPermissionsToCustomerEvent(this.Id , this.Email);
        this.AddDomainEvent(AddPermissionDomainEvent);
    }
    public void RevokePermission(List<string> permissions)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
        foreach(var i in permissions)
        {
            if (list.Contains(i))
            {
                list.Remove(i);
            }
        }
        PermissionJson = JsonSerializer.Serialize(list);
        this.UpdatedAt = DateTime.UtcNow;
        var RevokePermissionsDomainEvent = new RevokePermissionCustomerEvent(this.Id , this.Email);
        this.AddDomainEvent(RevokePermissionsDomainEvent);
    }
}