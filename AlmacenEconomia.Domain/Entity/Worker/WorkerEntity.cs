using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Common.WorkersType;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Events.Worker.AddPermission;
using AlmacenEconomia.Domain.Events.Worker.Create;
using AlmacenEconomia.Domain.Events.Worker.RevokePermission;
using AlmacenEconomia.Domain.Events.Worker.UpdateJob;
using AlmacenEconomia.Domain.Events.Worker.UpdatePassword;

namespace AlmacenEconomia.Domain.Entity.Worker;

public class WorkerEntity : GenericEntity<WorkerEntity>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Job { get; set; } = string.Empty;
    public string PermissionJson { get; set; } = "[]";
    [NotMapped]
    public IReadOnlyList<string> Permission => JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
    public static WorkerEntity Create(string email, string password, string job)
    {
        var worker = new WorkerEntity
        {
            Email = email,
            Job = job,
            Password = password
        };
        var permissions = new List<string>
        {
            Permissions.DeleteWorkerPermission,
            Permissions.GetOnlyWorkerPermission,
            Permissions.Auth,
            Permissions.GetAllProductsPermission,
            Permissions.GetOnlyProductPermission,
            Permissions.GetAllComboPermission,
            Permissions.GetOnlyComboPermission,
            Permissions.GetAllOfferPermission,
            Permissions.GetOnlyOfferPermission
        };
        if (job == WorkersType.Financial)
        {
            permissions.Add(Permissions.CreateProductPermission);
            permissions.Add(Permissions.UpdateProductPermission);
            permissions.Add(Permissions.DeleteProductPermission);
            permissions.Add(Permissions.GetAllSections);
            permissions.Add(Permissions.CreateProductEnterPermission);
            permissions.Add(Permissions.UpdateProductEnterPermission);
            permissions.Add(Permissions.GetOnlyProductEnterPermission);
            permissions.Add(Permissions.GetAllProductEnterPermission);
            permissions.Add(Permissions.GetEnterByProductIdPermission);
            permissions.Add(Permissions.CreateHomeSalePermission);
            permissions.Add(Permissions.UpdateHomeSalePermission);
            permissions.Add(Permissions.GetOnlyHomeSalePermission);
            permissions.Add(Permissions.GetAllHomeSalePermission);
            permissions.Add(Permissions.GetHomeSaleByProductId);
        }
        worker.PermissionJson = JsonSerializer.Serialize(permissions);
        var CreateWorkerDomainEvent = new CreateWorkerEntityEvent(worker.Email, worker.Id);
        worker.AddDomainEvent(CreateWorkerDomainEvent);
        return worker;
    }
    public void Update(string password)
    {
        Password = password;
        UpdatedAt = DateTime.UtcNow;
        var UpdatePasswordDomainEvent = new UpdateWorkerPasswordEvent(this.Email, this.Id);
        AddDomainEvent(UpdatePasswordDomainEvent);
    }
    public void AddPermission(List<string> permissions)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
        foreach (var i in permissions)
        {
            if (!list.Contains(i))
            {
                list.Add(i);
            }
        }
        PermissionJson = JsonSerializer.Serialize(list);
        this.UpdatedAt = DateTime.UtcNow;
        var AddPermissionDomainEvent = new AddWorkerPermissionEvent(this.Id, this.Email);
        this.AddDomainEvent(AddPermissionDomainEvent);
    }
    public void RevokePermission(List<string> permissions)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionJson) ?? new List<string>();
        foreach (var i in permissions)
        {
            if (list.Contains(i))
            {
                list.Remove(i);
            }
        }
        PermissionJson = JsonSerializer.Serialize(list);
        this.UpdatedAt = DateTime.UtcNow;
        var RevokePermissionDomainEvent = new RevokeWorkerPermissionEvent(this.Id, this.Email);
        this.AddDomainEvent(RevokePermissionDomainEvent);
    }
    public void UpdateJob(string job)
    {
        this.Job = job;
        this.UpdatedAt = DateTime.UtcNow;
        var UpdateJobDomainEvent = new UpdateWorkerJobEvent(this.Id, this.Email);
        this.AddDomainEvent(UpdateJobDomainEvent);
    }
}
